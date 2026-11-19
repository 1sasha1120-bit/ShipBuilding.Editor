using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.Models.ShipyardModels
{
    public class ShipyardEditingModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = StringLengthMessage)]
        public string? Name { get; set; }

        [Display(Name = CountryNameCaption)]
        [StringLength(CountryNameNameMaxLength, MinimumLength = CountryNameNameMinLength,
            ErrorMessage = StringLengthMessage)]
        public string? CountryName { get; set; }

        [Display(Name = YearOfFoundedCaption)]
        [Range(YearOfFoundedMinLength, 2100,
            ErrorMessage = RangeMessage)]
        public int? YearOfFounded { get; set; }

        [Display(Name = NumberOfDocksCaption)]
        [Range(NumberOfDocksMinLength, 100,
            ErrorMessage = RangeMessage)]
        public int? NumberOfDocks { get; set; }

        [Display(Name = CapacityPerYearCaption)]
        [Range(0, double.MaxValue,
            ErrorMessage = NonNegativeMessage)]
        public double? CapacityPerYear { get; set; }

        public static explicit operator ShipyardEditingModel(Shipyard obj) =>
            new ShipyardEditingModel
            {
                Id              = obj.Id,
                Name            = obj.Name,
                CountryName     = obj.CountryName,
                YearOfFounded   = obj.YearOfFounded,
                NumberOfDocks   = obj.NumberOfDocks,
                CapacityPerYear = obj.CapacityPerYear
            };
    }
}

