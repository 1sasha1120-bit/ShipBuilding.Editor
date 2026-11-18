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

        //[Display(Name = $"{HeightCaption}")]
        //public double? Height { get; set; }

        //[Display(Name = $"{WidthCaption}")]
        //public double? Width { get; set; }

        //[Display(Name = $"{LengthCaption}")]
        //public double? Length { get; set; }

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

                //Height = obj.Height,
                //Width = obj.Width,
                //Length = obj.Length,

                ShipyardName = obj.Shipyard?.Name,
                TypeOfShipName = obj.TypeOfShip?.Name,
            };
        }

    }
}