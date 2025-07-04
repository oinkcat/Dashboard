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
/// Загрузчик данных панели показателей
/// </summary>
public class DashboardDataLoader
{
    private const int CacheSeconds = 10;

    // Путь к каталогу хранения разметки
    private readonly string storagePath;

    // Кэш данных в памяти
    private readonly IMemoryCache memCache;

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
            var items = memCache.GetOrCreate($"data_{page.DataSource}", item =>
            {
                item.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CacheSeconds);

                string dataFileName = $"data_{page.DataSource}.json";
                string filePath = Path.Combine(storagePath, dataFileName);
                string content = File.ReadAllText(filePath);

                DataItemConverter converters = new();
                return JsonConvert.DeserializeObject<DataItem[]>(content, converters)
                    .ToDictionary(it => it.Id);
            });

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

    public DashboardDataLoader(IMemoryCache cache, StorageConfig config)
    {
        memCache = cache;
        storagePath = config.GetStoragePath();
    }
}
