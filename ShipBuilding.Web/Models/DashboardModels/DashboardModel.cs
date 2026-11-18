using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.DashboardModels
{
    public class DashboardModel
    {
        [Display(Name = TypeOfShipsHeader)]
        public int TypesCount { get; init; }

        [Display(Name = ShipyardsHeader)]
        public int ShipyardsCount { get; init; }

        [Display(Name = ProjectsHeader)]
        public int ProjectsCount { get; init; }

        [Display(Name = ShipsHeader)]
        public int ShipsCount { get; init; }

        [Display(Name = ShipsByStatusHeader)]
        public IReadOnlyList<StatusShare> ShipsByStatus { get; init; } = [];

        [Display(Name = TopShipyardsHeader)]
        public IReadOnlyList<ShipyardSummary> Shipyards { get; init; } = [];

        [Display(Name = LatestShipsHeader)]
        public IReadOnlyList<LatestShip> LatestShips { get; init; } = [];

        [Display(Name = BudgetHeader)]
        public BudgetSummary? Budget { get; init; }

        [Display(Name = ProjectsWithoutShipsHeader)]
        public IReadOnlyList<ProjectInfo> ProjectsWithoutShips { get; init; } = [];

        [Display(Name = ShipsByCategoryHeader)]
        public IReadOnlyList<CategoryShare> ShipsByCategory { get; init; } = [];
    }
}
