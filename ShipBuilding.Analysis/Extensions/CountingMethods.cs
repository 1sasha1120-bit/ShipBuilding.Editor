using ShipBuilding.Data.Interfaces;
using ShipBuilding.Statistics.DataQuality;

namespace ShipBuilding.Analysis.Extensions
{
    public static class CountingMethods
    {
        public static IReadOnlyDictionary<EntityKind, int> GetEntityCounts(this IReadOnlyDataSet data) =>
            new Dictionary<EntityKind, int>
            {
                [EntityKind.TypeOfShip] = data.Types.Count(),
                [EntityKind.Shipyard] = data.Shipyards.Count(),
                [EntityKind.Project] = data.ShipProjects.Count(),
                [EntityKind.Ship] = data.Ships.Count()
            };
    }
}
