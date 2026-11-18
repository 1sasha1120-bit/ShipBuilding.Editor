using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models;
using System.Diagnostics;

namespace ShipBuilding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public HomeController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult Index()
        {
            return View(_statisticsRepository.GetDashboard());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
