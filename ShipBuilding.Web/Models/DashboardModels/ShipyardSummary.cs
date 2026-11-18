using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.DashboardModels
{
    public record ShipyardSummary(
        int Id,
        [property: Display(Name = ShipyardCaption)] string? Name,
        [property: Display(Name = CountryCaption)] string? CountryName,
        [property: Display(Name = ShipsCountCaption)] int ShipsCount,
        [property: Display(Name = ProjectsCountCaption)] int ProjectsCount);
}
