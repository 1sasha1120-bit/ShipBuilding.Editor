using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Statistics.Dashboard
{
    public record LatestShip(
        int Id,
        [property: Display(Name = ShipCaption)] string? Name,
        [property: Display(Name = ShortLaunchDateCaption)] DateTime? LaunchDate,
        [property: Display(Name = TypeOfShipCaption)] string? TypeOfShipName);
}
