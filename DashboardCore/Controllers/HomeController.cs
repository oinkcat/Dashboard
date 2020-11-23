using DashboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DashboardCore.Services;

namespace DashboardCore.Controllers
{
    public class HomeController : Controller
    {
        // Имя загружаемой панели
        private const string PanelName = "default";

        private readonly DashboardStructureLoader dashboardLoader;

        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Действие отображения страницы панели
        /// </summary>
        public IActionResult Index()
        {
            var dashboardPage = dashboardLoader.LoadFromFile(PanelName)[0];

            return View(dashboardPage);
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
            DashboardStructureLoader dashboardLoader
        ) {
            this.dashboardLoader = dashboardLoader;
            this.logger = logger;
        }
    }
}
