using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipProjectModels
{
    public class ShipProjectDeletingModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [Display(Name = "Код проєкту")]
        public string? ProjectCode { get; set; }

        [Display(Name = ShipyardCaption)]
        public string? ShipyardName { get; set; }

        [Display(Name = DescriptionCaption)]
        public string? Description { get; set; }
    }
}
