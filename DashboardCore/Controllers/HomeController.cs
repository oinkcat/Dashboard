using DashboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using DashboardCore.Services;

namespace DashboardCore.Controllers;

/// <summary>
/// Основной контроллер
/// </summary>
public class HomeController : Controller
{
    // Имя загружаемой панели
    private const string PanelName = "default";

    private readonly DashboardStructureLoader structureLoader;
    private readonly DashboardDataLoader dataLoader;

    private readonly ILogger<HomeController> logger;

    /// <summary>
    /// Действие отображения страницы панели
    /// </summary>
    public IActionResult Index()
    {
        var dashboard = structureLoader.Load(PanelName);
        dataLoader.LoadDataForDashboard(dashboard);

        return View(dashboard.Pages.First());
    }

    [ResponseCache(Duration = 0)]
    public IActionResult Error()
    {
        logger.LogError("Exception ocurred!");

        return View(new ErrorViewModel { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
        });
    }

    public HomeController (
        ILogger<HomeController> logger, 
        DashboardStructureLoader structureLoader,
        DashboardDataLoader dataLoader
    ) {
        this.logger = logger;
        this.structureLoader = structureLoader;
        this.dataLoader = dataLoader;
    }
}
