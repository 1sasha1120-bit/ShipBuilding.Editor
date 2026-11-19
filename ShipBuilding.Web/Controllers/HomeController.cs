using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Infrastructure;
using ShipBuilding.Web.Infrastructure.Paging;
using ShipBuilding.Web.Models.ShipProjectModels;
using ShipBuilding.Web.Models.ShipyardModels;
using ShipBuilding.Web.Models.Ships;
using System.Diagnostics;

namespace ShipBuilding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IStatisticsRepository statisticsRepository, IUnitOfWork unitOfWork)
        {
            _statisticsRepository = statisticsRepository;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_statisticsRepository.GetDashboard());
        }

        public IActionResult TypeOfShips(int page = 1)
        {
            // Плоский список у порядку ієрархії: батьківський тип, за ним його підтипи
            var model = TreeBuilder.BuildFlatList(_unitOfWork.TypeOfShipsRepository.GetAll())
                .ToPagedList(page);
            return View(model);
        }

        public IActionResult Shipyards(int page = 1)
        {
            var model = _unitOfWork.ShipyardsRepository.GetAll()
                .Select(e => (ShipyardBrowsingModel)e)
                .OrderBy(e => e.Name)
                .ToPagedList(page);
            return View(model);
        }

        public IActionResult ShipProjects(int page = 1)
        {
            var model = _unitOfWork.ShipProjectsRepository.GetAll()
                .Select(e => (ShipProjectBrowsingModel)e)
                .OrderBy(e => e.Name)
                .ToPagedList(page);
            return View(model);
        }

        public IActionResult Ships(int page = 1)
        {
            var model = _unitOfWork.ShipsRepository.GetAll()
                .Select(e => (ShipBrowsingModel)e)
                //.OrderBy(e => e.Name)
                .ToPagedList(page);
            return View(model);
        }
    }
}
