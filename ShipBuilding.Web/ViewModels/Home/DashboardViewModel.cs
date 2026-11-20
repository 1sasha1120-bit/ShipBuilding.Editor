using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.ViewModels.Home
{
    /// <summary>
    /// Модель головної сторінки. Містить лише те, що показує сторінка, разом із підписами;
    /// заповнюється в HomeController з даних сервісу статистики.
    /// </summary>
    public class DashboardViewModel
    {
        [Display(Name = TypeOfShipsHeader)]
        public int TypesCount { get; set; }

        [Display(Name = ShipyardsHeader)]
        public int ShipyardsCount { get; set; }

        [Display(Name = ProjectsHeader)]
        public int ProjectsCount { get; set; }

        [Display(Name = ShipsHeader)]
        public int ShipsCount { get; set; }

        [Display(Name = ShipsByStatusHeader)]
        public IReadOnlyList<StatusItem> ShipsByStatus { get; set; } = [];

        /// <summary>Бюджет проєктів; null, якщо в жодного проєкту бюджет не задано.</summary>
        [Display(Name = BudgetHeader)]
        public BudgetItem? Budget { get; set; }

        [Display(Name = TopShipyardsHeader)]
        public IReadOnlyList<ShipyardItem> Shipyards { get; set; } = [];

        [Display(Name = LatestShipsHeader)]
        public IReadOnlyList<ShipItem> LatestShips { get; set; } = [];

        [Display(Name = ProjectsWithoutShipsHeader)]
        public IReadOnlyList<ProjectItem> ProjectsWithoutShips { get; set; } = [];

        // ---------------------------------------------------------------------------

        /// <summary>Частка суден з одним статусом.</summary>
        public class StatusItem
        {
            [Display(Name = StatusCaption)]
            public string Name { get; set; } = "";

            public int Count { get; set; }

            public double Percent { get; set; }

            /// <summary>Колір Bootstrap для статусу (success, warning…).</summary>
            public string Color { get; set; } = "light";
        }

        public class BudgetItem
        {
            [Display(Name = TotalBudgetCaption)]
            public decimal Total { get; set; }

            [Display(Name = AverageBudgetCaption)]
            public decimal Average { get; set; }

            [Display(Name = ProjectsWithBudgetCaption)]
            public int ProjectsWithBudget { get; set; }

            [Display(Name = MostExpensiveProjectCaption)]
            public ProjectItem? MostExpensive { get; set; }
        }

        public class ShipyardItem
        {
            [Display(Name = ShipyardCaption)]
            public string? Name { get; set; }

            [Display(Name = CountryCaption)]
            public string? CountryName { get; set; }

            [Display(Name = ShipsCountCaption)]
            public int ShipsCount { get; set; }

            [Display(Name = ProjectsCountCaption)]
            public int ProjectsCount { get; set; }
        }

        public class ShipItem
        {
            [Display(Name = ShipCaption)]
            public string? Name { get; set; }

            [Display(Name = TypeOfShipCaption)]
            public string? TypeOfShipName { get; set; }

            [Display(Name = ShortLaunchDateCaption)]
            public DateTime? LaunchDate { get; set; }
        }

        public class ProjectItem
        {
            [Display(Name = ProjectCaption)]
            public string? Name { get; set; }

            [Display(Name = ProjectCodeCaption)]
            public string? ProjectCode { get; set; }

            [Display(Name = ShipyardCaption)]
            public string? ShipyardName { get; set; }

            [Display(Name = BudgetCaption)]
            public decimal? Budget { get; set; }
        }
    }
}
