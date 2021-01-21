using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DashboardCore.Models;
using DashboardCore.Models.Structure;

namespace DashboardCore.Services
{
    /// <summary>
    /// Загрузчик данных панели показателей
    /// </summary>
    public class DashboardDataLoader
    {
        // Путь к каталогу хранения разметки
        private readonly string storagePath;

        /// <summary>
        /// Загрузить данные элементов
        /// </summary>
        /// <param name="dashboard">Панель показателей</param>
        public void LoadDataForDashboard(Dashboard dashboard)
        {
            foreach(var page in dashboard.Pages)
            {
                LoadDataForPage(page);
            }
        }

        // Загрузить данные для элементов страницы
        private void LoadDataForPage(DashboardPage page)
        {
            if (!String.IsNullOrEmpty(page.DataSource))
            {
                string dataFileName = $"data_{page.DataSource}.json";
                string filePath = Path.Combine(storagePath, dataFileName);
                string content = File.ReadAllText(filePath);

                var converter = new DataItemConverter();

                var items = JsonConvert.DeserializeObject<DataItem[]>(content, converter)
                    .ToDictionary(it => it.Id);

                foreach (var elem in page.Sections.SelectMany(sect => sect.Indicators))
                {
                    if (String.IsNullOrEmpty(elem.DataItem)) { continue; }

                    if (items.ContainsKey(elem.DataItem))
                    {
                        elem.UpdateData(items[elem.DataItem]);
                    }
                }
            }
        }

        public DashboardDataLoader(StorageConfig config)
        {
            storagePath = config.GetStoragePath();
        }
    }
}
