using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.Ships
{
    public class ShipBrowsingModel
    {
        //[HiddenInput(DisplayValue = false)]
        //[ScaffoldColumn(false)]
        //[Key]
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

        public static explicit operator ShipBrowsingModel(Ship obj)
        {
            return new ShipBrowsingModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                RegistrationNumber = obj.RegistrationNumber,
                LaunchDate = obj.LaunchDate,
                ShipyardName = obj.Shipyard?.Name,
                TypeOfShipName = obj.TypeOfShip?.Name,
            };
        }

    }
}