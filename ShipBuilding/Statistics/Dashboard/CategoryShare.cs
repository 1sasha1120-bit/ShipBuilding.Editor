using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Statistics.Dashboard
{ 
    public record CategoryShare(
        int? Id,
        [property: Display(Name = CategoryCaption)] string? Name,
        [property: Display(Name = CountCaption)] int Count,
        [property: Display(Name = PercentCaption)] double Percent);

    
}
