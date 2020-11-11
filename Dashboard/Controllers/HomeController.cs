using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        // Имя загружаемой панели
        private const string PanelName = "default";

        /// <summary>
        /// Обработчик действия запроса главной страницы
        /// </summary>
        public ActionResult Index()
        {
            var loader = new DashboardStructureLoader();
            var dashboardPage = loader.LoadFromFile(PanelName)[0];

            return View(dashboardPage);
        }
    }
}