using System;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace DashboardCore.Services;

/// <summary>
/// Содержит параметры хранения данных
/// </summary>
public class StorageConfig
{
    // Каталог для хранения файлов разметки панели
    private const string StorageDirectory = "Storage";

    private readonly IHostEnvironment env;

    /// <summary>
    /// Физический путь к каталогу хранения файлов
    /// </summary>
    public string GetStoragePath() => Path.Combine(env.ContentRootPath, StorageDirectory);

    public StorageConfig(IHostEnvironment env)
    {
        this.env = env;
    }
}