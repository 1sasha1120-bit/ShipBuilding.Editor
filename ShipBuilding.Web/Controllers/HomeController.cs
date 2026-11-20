using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.ShipProjects;
using ShipBuilding.Services.Ships;
using ShipBuilding.Services.Shipyards;
using ShipBuilding.Services.Statistics;
using ShipBuilding.Services.TypeOfShips;
using ShipBuilding.Types;
using ShipBuilding.Types.Extensions;
using ShipBuilding.Web.ViewModels.Home;
using ShipBuilding.Web.ViewModels.ShipProjects;
using ShipBuilding.Web.ViewModels.Shipyards;
using ShipBuilding.Web.ViewModels.Ships;
using ShipBuilding.Web.ViewModels.TypeOfShips;

namespace ShipBuilding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService _statistics;
        private readonly ITypeOfShipsService _types;
        private readonly IShipyardsService _shipyards;
        private readonly IShipProjectsService _projects;
        private readonly IShipsService _ships;
        private readonly IMapper _mapper;

        public HomeController(IStatisticsService statistics, ITypeOfShipsService types,
            IShipyardsService shipyards, IShipProjectsService projects, IShipsService ships,
            IMapper mapper)
        {
            _statistics = statistics;
            _types      = types;
            _shipyards  = shipyards;
            _projects   = projects;
            _ships      = ships;
            _mapper     = mapper;
        }

        public IActionResult Index()
        {
            var model = ToViewModel(_statistics.GetDashboard());

            return View(model);
        }

        public IActionResult TypeOfShips()
        {
            // Плоский список у порядку ієрархії: батьківський тип, за ним його підтипи
            var model = _mapper.Map<List<TypeOfShipBrowsingViewModel>>(_types.GetList(new TypeOfShipsQuery()));
            return View(model);
        }

        public IActionResult Shipyards()
        {
            // Порожній запит — усі верфі, упорядковані за назвою
            var model = _mapper.Map<List<ShipyardBrowsingViewModel>>(_shipyards.GetList(new ShipyardsQuery()));
            return View(model);
        }

        public IActionResult ShipProjects()
        {
            var model = _mapper.Map<List<ShipProjectBrowsingViewModel>>(_projects.GetList(new ShipProjectsQuery()));
            return View(model);
        }

        public IActionResult Ships()
        {
            var model = _mapper.Map<List<ShipBrowsingViewModel>>(_ships.GetList(new ShipsQuery()));
            return View(model);
        }

        // ---------------------------------------------------------------------------
        // Дані сервісу статистики → модель головної сторінки

        private static DashboardViewModel ToViewModel(DashboardModel dashboard)
        {
            var model = new DashboardViewModel();

            model.TypesCount = dashboard.TypesCount;
            model.ShipyardsCount = dashboard.ShipyardsCount;
            model.ProjectsCount = dashboard.ProjectsCount;
            model.ShipsCount = dashboard.ShipsCount;

            model.ShipsByStatus = dashboard.ShipsByStatus
                .Select(s => new DashboardViewModel.StatusItem
                {
                    Name = TypesMethods.GetDisplayName(s.Status),
                    Count = s.Count,
                    Percent = s.Percent,
                    Color = StatusColor(s.Status)
                })
                .ToList();

            if (dashboard.ProjectsWithBudget > 0)
            {
                model.Budget = new DashboardViewModel.BudgetItem
                {
                    Total = dashboard.TotalBudget,
                    Average = dashboard.AverageBudget,
                    ProjectsWithBudget = dashboard.ProjectsWithBudget,
                    MostExpensive = dashboard.MostExpensiveProject is ShipProjectDto top
                        ? ToProjectItem(top)
                        : null
                };
            }

            model.Shipyards = dashboard.Shipyards
                .Select(y => new DashboardViewModel.ShipyardItem
                {
                    Name = y.Name,
                    CountryName = y.CountryName,
                    ShipsCount = y.ShipsCount,
                    ProjectsCount = y.ProjectsCount
                })
                .ToList();

            model.LatestShips = dashboard.LatestShips
                .Select(s => new DashboardViewModel.ShipItem
                {
                    Name = s.Name,
                    TypeOfShipName = s.TypeOfShipName,
                    LaunchDate = s.LaunchDate
                })
                .ToList();

            model.ProjectsWithoutShips = dashboard.ProjectsWithoutShips
                .Select(ToProjectItem)
                .ToList();

            return model;
        }

        private static DashboardViewModel.ProjectItem ToProjectItem(ShipProjectDto project) =>
            new()
            {
                Name = project.Name,
                ProjectCode = project.ProjectCode,
                ShipyardName = project.ShipyardName,
                Budget = project.Budget
            };

        private static string StatusColor(ShipStatus status) => status switch
        {
            ShipStatus.InService      => "success",
            ShipStatus.UnderRepair    => "warning",
            ShipStatus.InReserve      => "secondary",
            ShipStatus.Decommissioned => "danger",
            _                         => "light"
        };
    }
}
