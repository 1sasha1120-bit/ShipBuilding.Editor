using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.AdminPanelModels
{
    public record IncompleteRecord(
        [property: Display(Name = EntityCaption)] string Section,
        string Controller,
        int Id,
        [property: Display(Name = NameCaption)] string? Name,
        [property: Display(Name = MissingFieldsCaption)] IReadOnlyList<string> MissingFields);
}
