using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ShipBuilding.Entities;
using ShipBuilding.Types;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipModels
{
    public class ShipsFilter
    {
        [UIHint("text")]
        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [UIHint("text")]
        [Display(Name = RegistrationNumberCaption)]
        public string? RegistrationNumber { get; set; }

        [Display(Name = LaunchDateCaption)]
        [DataType(DataType.Date)]
        public DateTime? LaunchDateMin { get; set; }

        [Display(Name = LaunchDateCaption)]
        [DataType(DataType.Date)]
        public DateTime? LaunchDateMax { get; set; }

        [Display(Name = ShipyardCaption)]
        public string? ShipyardName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ShipyardNamesList { get; set; }

        [Display(Name = TypeOfShipCaption)]
        public string? TypeOfShipName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? TypeOfShipNamesList { get; set; }
    }
}
