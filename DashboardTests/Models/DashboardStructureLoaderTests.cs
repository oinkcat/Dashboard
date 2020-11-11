using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dashboard.Models.Tests
{
    /// <summary>
    /// Тест загрузки панели
    /// </summary>
    [TestClass()]
    public class DashboardStructureLoaderTests
    {
        // Физический путь к каталогу хранения структуры
        private const string StoragePath = @"C:\Users\softc\source\repos\" +
                                           @"Dashboard\Dashboard\Storage";

        // Название панели по умолчанию
        private const string DefaultPanelName = "default";

        /// <summary>
        /// Тест загрузки структуры панели из файла
        /// </summary>
        [TestMethod()]
        public void LoadFromFileTest()
        {
            var panelLoader = new DashboardStructureLoader(StoragePath);

            var layout = panelLoader.LoadFromFile(DefaultPanelName);
            var firstPage = layout[0];

            Assert.IsNotNull(firstPage.Sections);
            Assert.IsNotNull(firstPage.Sections[0].Indicators);
        }
    }
}