using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.ViewModels.Ships
{
    public class ShipBrowsingViewModel
    {
        public int Id { get; set; }

        [Display(Name = $"{NameCaption}")]
        public string? Name { get; set; }

        [Display(Name = $"{RegistrationNumberCaption}")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = $"{LaunchDateCaption}")]
        [DataType(DataType.Date)]
        public DateTime? LaunchDate { get; set; }

        [Display(Name = $"{ShipyardCaption}")]
        public string? ShipyardName { get; set; }

        [Display(Name = $"{TypeOfShipCaption}")]
        public string? TypeOfShipName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ShipyardSelectList { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? TypeOfShipSelectList { get; set; }
    }
}