using ShipBuilding.Entities;
using ShipBuilding.Data.Interfaces;
using ShipBuilding.Types;
using ShipBuilding.Statistics.Dashboard;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Analysis.Extensions
{
    public static class DashboardMethods
    {
        public static DashboardModel GetDashboardModel(this IReadOnlyDataSet data)
        {
            var types = data.Types.ToList();
            var shipyards = data.Shipyards.ToList();
            var projects = data.ShipProjects.ToList();
            var ships = data.Ships.ToList();
            bool hasSingleRoot = types.Count(t => t.Parent is null) == 1;

            return new DashboardModel
            {
                TypesCount = types.Count,
                ShipyardsCount = shipyards.Count,
                ProjectsCount = projects.Count,
                ShipsCount = ships.Count,

                ShipsByStatus = ships
                    .GroupBy(e => e.Status)
                    .OrderBy(g => g.Key == ShipStatus.Unknown ? int.MaxValue : (int)g.Key)
                    .Select(g => new StatusShare(g.Key, g.Count(),
                        100.0 * g.Count() / ships.Count))
                    .ToList(),

                Shipyards = shipyards
                    .Select(y => new ShipyardSummary(y.Id, y.Name, y.CountryName,
                        ships.Count(s => ReferenceEquals(s.Shipyard, y)),
                        projects.Count(p => ReferenceEquals(p.Shipyard, y))))
                    .OrderByDescending(y => y.ShipsCount)
                    .ThenBy(y => y.Name)
                    .Take(5)
                    .ToList(),

                LatestShips = ships
                    .Where(e => e.LaunchDate.HasValue)
                    .OrderByDescending(e => e.LaunchDate)
                    .Take(5)
                    .Select(e => new LatestShip(e.Id, e.Name, e.LaunchDate, e.TypeOfShip?.Name))
                    .ToList(),

                Budget = projects.ToBudgetSummary(),

                ProjectsWithoutShips = projects
                    .Where(p => !ships.Any(s => s.Project?.Id == p.Id))
                    .OrderBy(p => p.Name)
                    .Select(p => p.ToProjectInfo())
                    .ToList(),

                ShipsByCategory = ships
                    .GroupBy(s => s.TypeOfShip.GetCategory(skipRoot: hasSingleRoot))
                    .Select(g => new CategoryShare(g.Key?.Id, g.Key?.Name ?? WithoutTypeCaption,
                        g.Count(), 100.0 * g.Count() / ships.Count))
                    .OrderBy(c => c.Id is null)          // "Без типу" — в кінці
                    .ThenByDescending(c => c.Count)
                    .ThenBy(c => c.Name)
                    .ToList()
            };
        }

        public static ProjectInfo ToProjectInfo(this ShipProject p) =>
            new(p.Id, p.Name, p.ProjectCode, p.Shipyard?.Name, p.Budget);

        public static BudgetSummary? ToBudgetSummary(this IEnumerable<ShipProject> projects)
        {
            var withBudget = projects.Where(p => p.Budget.HasValue).ToList();
            if (withBudget.Count == 0) return null;

            var mostExpensive = withBudget.MaxBy(p => p.Budget!.Value)!;
            return new BudgetSummary(
                Total: withBudget.Sum(p => p.Budget!.Value),
                Average: withBudget.Average(p => p.Budget!.Value),
                ProjectsWithBudget: withBudget.Count,
                MostExpensive: mostExpensive.ToProjectInfo());
        }

        public static TypeOfShip? GetCategory(this TypeOfShip? type, bool skipRoot)
        {
            if (type is null) return null;

            var path = new List<TypeOfShip>();          // від типу до кореня
            var visited = new HashSet<TypeOfShip>(ReferenceEqualityComparer.Instance);
            for (var t = type; t is not null && visited.Add(t); t = t.Parent)
                path.Add(t);

            return skipRoot && path.Count >= 2 ? path[^2] : path[^1];
        }
    }
}
