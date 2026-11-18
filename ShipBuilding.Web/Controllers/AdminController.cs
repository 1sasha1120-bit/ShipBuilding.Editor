using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models.Extensions;

namespace ShipBuilding.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public AdminController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult Index()
        {
            return View(_statisticsRepository.GetAdminPanelModel());
        }
    }
}
