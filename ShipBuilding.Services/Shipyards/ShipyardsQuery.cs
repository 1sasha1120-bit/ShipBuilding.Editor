using Common.Types;

namespace ShipBuilding.Services.Shipyards
{
    public enum ShipyardSortField
    {
        Name, CountryName, YearOfFounded, NumberOfDocks, CapacityPerYear
    }

    /// <summary>
    /// Критерії пошуку верфей. Незадані критерії не обмежують вибірку.
    /// </summary>
    public record ShipyardsQuery
    {
        public string? Name { get; init; }
        public string? CountryName { get; init; }
        public int? YearOfFoundedFrom { get; init; }
        public int? YearOfFoundedTo { get; init; }

        public ShipyardSortField SortBy { get; init; } = ShipyardSortField.Name;
        public SortingOrder Order { get; init; } = SortingOrder.Ascending;
    }
}
