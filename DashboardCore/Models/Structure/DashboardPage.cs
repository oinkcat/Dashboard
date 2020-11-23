using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DashboardCore.Models.Structure
{
    /// <summary>
    /// Страница для отображения
    /// </summary>
    public class DashboardPage
    {
        /// <summary>
        /// Идентификатор страницы
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Разделы показателей
        /// </summary>
        public List<Section> Sections { get; set; }

        /// <summary>
        /// Имя источника данных
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// Загрузить данные элементов
        /// </summary>
        /// <param name="dataDirectory">Каталог файлов данных</param>
        public void LoadData(string dataDirectory)
        {
            if(!String.IsNullOrEmpty(DataSource))
            {
                string filePath = Path.Combine(dataDirectory, $"data_{DataSource}.json");
                string content = File.ReadAllText(filePath);

                var converter = new DataItemConverter();

                var items = JsonConvert.DeserializeObject<DataItem[]>(content, converter)
                    .ToDictionary(it => it.Id);

                foreach(var elem in Sections.SelectMany(sect => sect.Indicators))
                {
                    if(String.IsNullOrEmpty(elem.DataItem)) { continue; }

                    if(items.ContainsKey(elem.DataItem))
                    {
                        elem.UpdateData(items[elem.DataItem]);
                    }
                }
            }
        }

        /// <summary>
        /// Посчитать ширину всех элементов страницы
        /// </summary>
        public void ComputeWidths()
        {
            Sections.ForEach(s => s.ComputeWidths());
        }
    }
}