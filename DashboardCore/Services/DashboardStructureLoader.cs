using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using DashboardCore.Models;
using DashboardCore.Models.Structure;
using Microsoft.Extensions.Caching.Memory;

namespace DashboardCore.Services;

/// <summary>
/// Загрузчик структуры панели администратора
/// </summary>
public class DashboardStructureLoader
{
    private const int CacheMinutes = 5;

    // Путь к каталогу хранения разметки
    private readonly string storagePath;

    // Кэш данных в памяти
    private readonly IMemoryCache memCache;

    /// <summary>
    /// Использовать кэш данных
    /// </summary>
    public bool UseCache { get; } = true;

    /// <summary>
    /// Загрузить разметку панели админа из файла
    /// </summary>
    /// <param name="panelName">Название панели для загрузки</param>
    /// <returns>Структура загруженной панели</returns>
    public Dashboard Load(string panelName)
    {
        return memCache.GetOrCreate(panelName, item =>
        {
            item.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheMinutes);
            return LoadFromFile(panelName);
        });
    }

    // Загрузить разметку панели из файла
    private Dashboard LoadFromFile(string panelName)
    {
        string filePath = Path.Combine(storagePath, $"{panelName}.json");
        string contents = File.ReadAllText(filePath);

        var panelConv = new PanelStructureConverter();
        var pages = JsonConvert.DeserializeObject<IList<DashboardPage>>(contents, panelConv);

        // Загрузить данные и посчитать ширину
        foreach (var page in pages)
        {
            page.ComputeWidths();
        }

        return new Dashboard(panelName, pages);
    }

    public DashboardStructureLoader(IMemoryCache cache, StorageConfig config)
    {
        memCache = cache;
        storagePath = config.GetStoragePath();
    }
}