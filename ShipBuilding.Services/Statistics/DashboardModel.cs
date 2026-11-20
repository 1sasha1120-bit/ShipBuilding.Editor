using ShipBuilding.Services.ShipProjects;
using ShipBuilding.Services.Ships;
using ShipBuilding.Types;

namespace ShipBuilding.Services.Statistics
{
    /// <summary>
    /// Дані головної сторінки. Судна й проєкти подаються тими самими знімками, що й у решті
    /// застосунку (<see cref="ShipDto"/>, <see cref="ShipProjectDto"/>); окремі записи —
    /// лише там, де потрібні підрахунки.
    /// </summary>
    public class DashboardModel
    {
        public int TypesCount { get; set; }

        public int ShipyardsCount { get; set; }

        public int ProjectsCount { get; set; }

        public int ShipsCount { get; set; }

        public IReadOnlyList<StatusShare> ShipsByStatus { get; set; } = [];

        public IReadOnlyList<ShipyardSummary> Shipyards { get; set; } = [];

        public IReadOnlyList<ShipDto> LatestShips { get; set; } = [];

        // Бюджет проєктів (рахується лише за проєктами, де бюджет задано)

        public decimal TotalBudget { get; set; }

        public decimal AverageBudget { get; set; }

        public int ProjectsWithBudget { get; set; }

        public ShipProjectDto? MostExpensiveProject { get; set; }

        public IReadOnlyList<ShipProjectDto> ProjectsWithoutShips { get; set; } = [];

        /// <summary>Скільки суден мають певний статус і яка це частка від усіх.</summary>
        public record StatusShare(ShipStatus Status, int Count, double Percent);

        /// <summary>Верф із кількістю суден і проєктів.</summary>
        public record ShipyardSummary(
            int Id,
            string? Name,
            string? CountryName,
            int ShipsCount,
            int ProjectsCount);
    }
}
