using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.DashboardModels
{
    public record ProjectInfo(
        int Id,
        [property: Display(Name = ProjectCaption)] string? Name,
        [property: Display(Name = ProjectCodeCaption)] string? ProjectCode,
        [property: Display(Name = ShipyardCaption)] string? ShipyardName,
        [property: Display(Name = BudgetCaption)] decimal? Budget);
}
