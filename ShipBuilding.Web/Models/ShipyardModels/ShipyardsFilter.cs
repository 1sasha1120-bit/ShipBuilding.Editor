using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipyardModels
{
    public class ShipyardsFilter
    {
        [UIHint("text")]
        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [UIHint("text")]
        [Display(Name = CountryNameCaption)]
        public string? CountryName { get; set; }

        [Display(Name = YearOfFoundedCaption)]
        public int? YearOfFoundedFrom { get; set; }

        [Display(Name = YearOfFoundedCaption)]
        public int? YearOfFoundedTo { get; set; }
    }
}
