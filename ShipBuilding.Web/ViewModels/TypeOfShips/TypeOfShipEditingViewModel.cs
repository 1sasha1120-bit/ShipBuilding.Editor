
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.ViewModels.TypeOfShips
{
    public class TypeOfShipEditingViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = StringLengthMessage)]
        public string? Name { get; set; }

        [Display(Name = TypeCodeCaption)]
        [RegularExpression(TypeCodeLimitation,
            ErrorMessage = TypeCodeErrorMessage)]
        public string? TypeCode { get; set; }

        [Display(Name = ParentCaption)]
        public string? ParentName { get; set; }

        [Display(Name = DescriptionCaption)]
        [StringLength(DescriptionMaxLength, MinimumLength = 0)]
        public string? Description { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ParentSelectList { get; set; }
    }
}

