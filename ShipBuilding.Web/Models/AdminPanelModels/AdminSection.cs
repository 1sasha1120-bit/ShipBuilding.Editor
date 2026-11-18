using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.AdminPanelModels
{
    public record AdminSection(
        string Title,
        string Description,
        string IconCss,
        string Controller,
        [property: Display(Name = RecordsCountCaption)] int Count);
}
