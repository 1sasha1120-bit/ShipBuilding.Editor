using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipProjectModels
{
    public class ShipProjectsFilter
    {
        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [Display(Name = "Код проєкту")]
        public string? ProjectCode { get; set; }

        [Display(Name = ShipyardCaption)]
        public string? ShipyardName { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList? ShipyardNamesList { get; set; }
    }
}
