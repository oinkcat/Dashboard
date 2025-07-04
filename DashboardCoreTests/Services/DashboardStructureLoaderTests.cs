using System;
using Microsoft.Extensions.Hosting;
using Xunit;
using Moq;
using DashboardCore.Services;

namespace DashboardCoreTests.Services
{
    /// <summary>
    /// Тестирование загрузки структуры панели
    /// </summary>
    public class DashboardStructureLoaderTests
    {
        // Физический путь к каталогу содержимого
        private const string StoragePath = @"..\..\..\..\DashboardCore";

        // Название панели по умолчанию
        private const string DefaultPanelName = "default";

        /// <summary>
        /// Тест загрузки структуры панели из файла
        /// </summary>
        [Fact]
        public void LoadFromFileTest()
        {
            var hostingEnvMoq = new Mock<IHostEnvironment>();
            hostingEnvMoq.SetupGet(env => env.ContentRootPath)
                .Returns(StoragePath);

            var storageConfSvc = new StorageConfig(hostingEnvMoq.Object);
            var panelLoader = new DashboardStructureLoader(storageConfSvc);

            var layout = panelLoader.Load(DefaultPanelName);
            var firstPage = layout.Pages[0];

            Assert.NotNull(firstPage.Sections);
            Assert.NotNull(firstPage.Sections[0].Indicators);
        }
    }
}
