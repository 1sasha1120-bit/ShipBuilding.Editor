using Common.Types;

namespace ShipBuilding.Services.Ships
{
    public enum ShipSortField
    {
        Name, RegistrationNumber, LaunchDate, ShipyardName, TypeOfShipName
    }

    /// <summary>
    /// Критерії пошуку суден. Незадані критерії не обмежують вибірку.
    /// </summary>
    public record ShipsQuery
    {
        public string? Name { get; init; }
        public string? RegistrationNumber { get; init; }
        public DateTime? LaunchDateMin { get; init; }
        public DateTime? LaunchDateMax { get; init; }
        public string? ShipyardName { get; init; }
        public string? TypeOfShipName { get; init; }

        public ShipSortField SortBy { get; init; } = ShipSortField.Name;
        public SortingOrder Order { get; init; } = SortingOrder.Ascending;
    }
}
