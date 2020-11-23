using System;
using Microsoft.Extensions.Hosting;
using Xunit;
using Moq;
using DashboardCore.Services;

namespace DashboardCoreTests.Services
{
    /// <summary>
    /// ������������ �������� ��������� ������
    /// </summary>
    public class DashboardStructureLoaderTests
    {
        // ���������� ���� � �������� �����������
        private const string StoragePath = @"C:\Users\softc\source\repos\" +
                                           @"Dashboard\DashboardCore";

        // �������� ������ �� ���������
        private const string DefaultPanelName = "default";

        /// <summary>
        /// ���� �������� ��������� ������ �� �����
        /// </summary>
        [Fact]
        public void LoadFromFileTest()
        {
            var hostingEnvMoq = new Mock<IHostEnvironment>();
            hostingEnvMoq.SetupGet(env => env.ContentRootPath)
                .Returns(StoragePath);

            var storageConfSvc = new StorageConfig(hostingEnvMoq.Object);
            var panelLoader = new DashboardStructureLoader(storageConfSvc);

            var layout = panelLoader.LoadFromFile(DefaultPanelName);
            var firstPage = layout[0];

            Assert.NotNull(firstPage.Sections);
            Assert.NotNull(firstPage.Sections[0].Indicators);
        }
    }
}
