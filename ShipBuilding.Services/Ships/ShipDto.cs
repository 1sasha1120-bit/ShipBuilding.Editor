using ShipBuilding.Entities;
using ShipBuilding.Types;

namespace ShipBuilding.Services.Ships
{
    public record ShipDto
    {
        public int Id { get; init; }
        public string Key { get; init; } = "";
        public string? Name { get; init; }
        public string? RegistrationNumber { get; init; }
        public DateTime? LaunchDate { get; init; }
        public ShipStatus Status { get; init; }
        public double? Height { get; init; }
        public double? Width { get; init; }
        public double? Length { get; init; }
        public string? ShipyardName { get; init; }
        public string? TypeOfShipName { get; init; }
        public string? ProjectName { get; init; }

        internal static ShipDto From(Ship ship)
        {
            return new()
            {
                Id                 = ship.Id,
                Key                = ship.Key,
                Name               = ship.Name,
                RegistrationNumber = ship.RegistrationNumber,
                LaunchDate         = ship.LaunchDate,
                Status             = ship.Status,
                Height             = ship.Height,
                Width              = ship.Width,
                Length             = ship.Length,
                ShipyardName       = ship.Shipyard?.Name,
                TypeOfShipName     = ship.TypeOfShip?.Name,
                ProjectName        = ship.Project?.Name
            };
        }
    }
}
