using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipyardModels
{
    public class ShipyardBrowsingModel
    {
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [Display(Name = CountryNameCaption)]
        public string? CountryName { get; set; }

        [Display(Name = YearOfFoundedCaption)]
        public int? YearOfFounded { get; set; }

        [Display(Name = NumberOfDocksCaption)]
        public int? NumberOfDocks { get; set; }

        [Display(Name = CapacityPerYearCaption)]
        public double? CapacityPerYear { get; set; }

        public static explicit operator ShipyardBrowsingModel(Shipyard obj) =>
            new ShipyardBrowsingModel
            {
                Id = obj.Id,
                Name = obj.Name,
                CountryName = obj.CountryName,
                YearOfFounded = obj.YearOfFounded,
                NumberOfDocks = obj.NumberOfDocks,
                CapacityPerYear = obj.CapacityPerYear
            };
    }
}
