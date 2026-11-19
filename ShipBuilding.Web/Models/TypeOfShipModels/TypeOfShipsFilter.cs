using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.TypeOfShipModels
{
    public class TypeOfShipsFilter
    {
        [UIHint("text")]
        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [UIHint("text")]
        [Display(Name = TypeCodeCaption)]
        public string? TypeCode { get; set; }
    }
}
