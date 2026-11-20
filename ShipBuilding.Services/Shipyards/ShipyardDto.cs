using ShipBuilding.Entities;

namespace ShipBuilding.Services.Shipyards
{
    /// <summary>
    /// Дані верфі: їх повертає сервіс і їх же він приймає для створення чи зміни.
    /// Key заповнюється лише у відповідях сервісу.
    /// </summary>
    public record ShipyardDto
    {
        public int Id { get; init; }
        public string Key { get; init; } = "";
        public string? Name { get; init; }
        public string? CountryName { get; init; }
        public int? YearOfFounded { get; init; }
        public int? NumberOfDocks { get; init; }
        public double? CapacityPerYear { get; init; }

        internal static ShipyardDto From(Shipyard shipyard)
        {
            return new()
            {
                Id              = shipyard.Id,
                Key             = shipyard.Key,
                Name            = shipyard.Name,
                CountryName     = shipyard.CountryName,
                YearOfFounded   = shipyard.YearOfFounded,
                NumberOfDocks   = shipyard.NumberOfDocks,
                CapacityPerYear = shipyard.CapacityPerYear
            };
        }
    }
}
