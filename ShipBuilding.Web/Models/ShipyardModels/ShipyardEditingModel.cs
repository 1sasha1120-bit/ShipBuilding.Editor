using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipyardModels
{
    public class ShipyardEditingModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        [Required(ErrorMessage = $"Потрібно заповнити поле '{NameCaption}'")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = $"Назва повинна містити від {{2}} до {{1}} символів")]
        public string? Name { get; set; }

        [Display(Name = CountryNameCaption)]
        [StringLength(CountryNameNameMaxLength, MinimumLength = CountryNameNameMinLength,
            ErrorMessage = $"Назва країни повинна містити від {{2}} до {{1}} символів")]
        public string? CountryName { get; set; }

        [Display(Name = YearOfFoundedCaption)]
        [Range(YearOfFoundedMinLength, 2100,
            ErrorMessage = $"Рік заснування має бути від {{1}} до {{2}}")]
        public int? YearOfFounded { get; set; }

        [Display(Name = NumberOfDocksCaption)]
        [Range(NumberOfDocksMinLength, 100,
            ErrorMessage = $"Кількість доків має бути від {{1}} до {{2}}")]
        public int? NumberOfDocks { get; set; }

        [Display(Name = CapacityPerYearCaption)]
        [Range(0, double.MaxValue,
            ErrorMessage = "Річна потужність не може бути від'ємною")]
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

