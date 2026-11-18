using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.TypeOfShipModels
{
    public class TypeOfShipBrowsingModel
    {
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [Display(Name = TypeCodeCaption)]
        public string? TypeCode { get; set; }

        [Display(Name = ParentCaption)]
        public string? ParentName { get; set; }

        //[Display(Name = DescriptionCaption)]
        //public string? Description { get; set; }

        // ----------------------------------------------------

        public int Depth { get; set; }
        public List<TypeOfShipBrowsingModel> Children { get; set; } = [];
        public bool HasChildren => Children.Count > 0;
    }
}
