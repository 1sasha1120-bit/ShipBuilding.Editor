using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Types;
using ShipBuilding.Web.Infrastructure.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.ViewModels.Ships
{
    public class ShipEditingViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }


        [Display(Name = NameCaption)]
        [Required(ErrorMessage =
             RequiredFieldMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
             ErrorMessage = StringLengthMessage)]
        public string? Name { get; set; }


        [Display(Name = $"{RegistrationNumberCaption}")]
        [Required(ErrorMessage =
             $"{RegistrationNumberErrorMessage}")]
        [RegularExpression(RegistrationNumberLimitation, ErrorMessage =
            RegistrationNumberErrorMessage)]
        public string? RegistrationNumber { get; set; }

        [Display(Name = LaunchDateCaption)]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage =
            LaunchDateInFutureMessage)]
        public DateTime? LaunchDate { get; set; }

        [Display(Name = StatusCaption)]
        [Required(ErrorMessage = RequiredSelectionMessage)]
        [Range(1, 4, ErrorMessage = RequiredSelectionMessage)]
        public ShipStatus Status { get; set; }

        [Display(Name = $"{HeightCaption}")]
        [Range(HeightOfShipMinLength, HeighthOfShipMaxLength, ErrorMessage = RangeMessage)]
        public double? Height { get; set; }


        [Display(Name = $"{WidthCaption}")]
        [Range(WidthOfShipMinLength, WidthOfShipMaxLength, ErrorMessage = RangeMessage)]
        public double? Width { get; set; }


        [Display(Name = $"{LengthCaption}")]
        [Range(LengthOfShipMinLength, LengthOfShipMaxLength, ErrorMessage = RangeMessage)]
        public double? Length { get; set; }


        [Display(Name = $"{ShipyardCaption}")]
        [Required(ErrorMessage =
     RequiredFieldMessage)]
        public string? ShipyardName { get; set; }


        [Display(Name = $"{TypeOfShipCaption}")]
        [Required(ErrorMessage =
    RequiredFieldMessage)]
        public string? TypeOfShipName { get; set; }

        [Display(Name = $"{ProjectCaption}")]
        public string? ProjectName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ShipyardSelectList { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? TypeOfShipSelectList { get; set; }
        [ScaffoldColumn(false)]
        public SelectList? ProjectSelectList { get; set; }
    }
}
