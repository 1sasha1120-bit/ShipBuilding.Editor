using ShipBuilding.Statistics.DataQuality;
using ShipBuilding.Data.Interfaces;
using ShipBuilding.Types;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Analysis.Extensions
{
    public static class DataQualityMethods
    {
        public static IReadOnlyList<IncompleteRecord> GetIncompleteRecords(this IReadOnlyDataSet data)
        {
            var types = data.Types.ToList();
            var shipyards = data.Shipyards.ToList();
            var projects = data.ShipProjects.ToList();
            var ships = data.Ships.ToList();

            var incomplete = new List<IncompleteRecord>();

            incomplete.AddRange(ships.Select(s => new IncompleteRecord(EntityKind.Ship, ShipsHeader, s.Id, s.Name,
                Missing(
                    (string.IsNullOrWhiteSpace(s.RegistrationNumber), RegistrationNumberCaption),
                    (s.LaunchDate is null, LaunchDateCaption),
                    (s.Status == ShipStatus.Unknown, StatusCaption),
                    (s.TypeOfShip is null, TypeOfShipCaption),
                    (s.Shipyard is null, ShipyardCaption)))));

            incomplete.AddRange(projects.Select(p => new IncompleteRecord(EntityKind.Project, ProjectsHeader, p.Id, p.Name,
                Missing(
                    (string.IsNullOrWhiteSpace(p.ProjectCode), ProjectCodeCaption),
                    (p.Budget is null, BudgetCaption),
                    (p.Shipyard is null, ShipyardCaption)))));

            incomplete.AddRange(shipyards.Select(y => new IncompleteRecord(EntityKind.Shipyard, ShipyardsHeader, y.Id, y.Name,
                Missing(
                    (string.IsNullOrWhiteSpace(y.CountryName), CountryCaption),
                    (y.YearOfFounded is null, YearOfFoundedCaption),
                    (y.NumberOfDocks is null, NumberOfDocksCaption),
                    (y.CapacityPerYear is null, CapacityPerYearCaption)))));

            // кореневий тип не має коду за задумом
            incomplete.AddRange(types.Select(t => new IncompleteRecord(EntityKind.TypeOfShip, TypeOfShipsHeader, t.Id, t.Name,
                Missing(
                    (t.Parent is not null && string.IsNullOrWhiteSpace(t.TypeCode), TypeCodeCaption),
                    (string.IsNullOrWhiteSpace(t.Description), DescriptionCaption)))));

            return incomplete
                .Where(r => r.MissingFields.Count > 0)
                .OrderByDescending(r => r.MissingFields.Count)
                .ThenBy(r => r.Section)
                .ThenBy(r => r.Name)
                .ToList();
        }

        private static IReadOnlyList<string> Missing(params (bool IsMissing, string Caption)[] checks) =>
            checks.Where(c => c.IsMissing).Select(c => c.Caption).ToList();
    }
}
