using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.ViewModels.ShipProjects
{
    public class ShipProjectsFilter
    {
        [UIHint("text")]
        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [UIHint("text")]
        [Display(Name = ProjectCodeCaption)]
        public string? ProjectCode { get; set; }

        [Display(Name = ShipyardCaption)]
        public string? ShipyardName { get; set; }

        public SelectList? ShipyardNamesList { get; set; }
    }
}
