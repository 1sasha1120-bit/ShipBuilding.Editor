using System.ComponentModel.DataAnnotations;
using ShipBuilding.Types;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Statistics.Dashboard
{
    public record StatusShare(
        [property: Display(Name = StatusCaption)] ShipStatus Status,
        [property: Display(Name = CountCaption)] int Count,
        [property: Display(Name = PercentCaption)] double Percent);
}
