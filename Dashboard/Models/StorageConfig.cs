using System;
using System.Web.Hosting;

namespace Dashboard.Models
{
    /// <summary>
    /// Содержит параметры хранения данных
    /// </summary>
    public static class StorageConfig
    {
        // Каталог для хранения файлов разметки панели
        private const string StorageDirectory = "Storage";

        /// <summary>
        /// Физический путь к каталогу хранения файлов
        /// </summary>
        public static string StoragePath =>
            HostingEnvironment.MapPath($"~/{StorageDirectory}");
    }
}