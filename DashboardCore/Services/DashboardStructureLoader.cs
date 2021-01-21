using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using DashboardCore.Models;
using DashboardCore.Models.Structure;

namespace DashboardCore.Services
{
    /// <summary>
    /// Загрузчик структуры панели администратора
    /// </summary>
    public class DashboardStructureLoader
    {
        // Путь к каталогу хранения разметки
        private readonly string storagePath;

        /// <summary>
        /// Загрузить разметку панели админа из файла
        /// </summary>
        /// <param name="panelName">Название панели для загрузки</param>
        /// <returns>Структура загруженной панели</returns>
        public Dashboard LoadFromFile(string panelName)
        {
            string filePath = Path.Combine(storagePath, $"{panelName}.json");
            string contents = File.ReadAllText(filePath);

            var panelConv = new PanelStructureConverter();
            var pages = JsonConvert.DeserializeObject<IList<DashboardPage>>(contents, panelConv);

            // Загрузить данные и посчитать ширину
            foreach(var page in pages)
            {
                page.ComputeWidths();
            }

            return new Dashboard(panelName, pages);
        }

        public DashboardStructureLoader(StorageConfig config)
        {
            storagePath = config.GetStoragePath();
        }
    }
}