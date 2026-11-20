using ShipBuilding.Types;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.ViewModels.Ships
{
    public class ShipDeletingViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = $"{NameCaption}")]
        public string? Name { get; set; }

        [Display(Name = $"{RegistrationNumberCaption}")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = $"{LaunchDateCaption}")]
        [DataType(DataType.Date)]
        public DateTime? LaunchDate { get; set; }

        [Display(Name = StatusCaption)]
        public ShipStatus Status { get; set; }

        [Display(Name = $"{HeightCaption}")]
        public double? Height { get; set; }

        [Display(Name = $"{WidthCaption}")]
        public double? Width { get; set; }

        [Display(Name = $"{LengthCaption}")]
        public double? Length { get; set; }

        [Display(Name = $"{ShipyardCaption}")]
        public string? ShipyardName { get; set; }

        [Display(Name = $"{TypeOfShipCaption}")]
        public string? TypeOfShipName { get; set; }

        [Display(Name = $"{ProjectCaption}")]
        public string? ProjectName { get; set; }
    }
}
