using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.TypeOfShipModels
{
    public class TypeOfShipsFilter
    {
        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [Display(Name = TypeCodeCaption)]
        public string? TypeCode { get; set; }
    }
}
