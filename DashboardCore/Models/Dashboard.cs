using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardCore.Models.Structure;

namespace DashboardCore.Models;

/// <summary>
/// Представление панели показателей
/// </summary>
public class Dashboard
{
    /// <summary>
    /// Название панели
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Страницы панели
    /// </summary>
    public List<DashboardPage> Pages { get; }

    public Dashboard(string name, IList<DashboardPage> pages)
    {
        Name = name;
        Pages = pages.ToList();
    }
}
