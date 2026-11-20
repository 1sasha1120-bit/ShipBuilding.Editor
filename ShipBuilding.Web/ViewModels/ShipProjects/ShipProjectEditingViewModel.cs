using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebTexts;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.ViewModels.ShipProjects
{
    public class ShipProjectEditingViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = StringLengthMessage)]
        public string? Name { get; set; }

        [Display(Name = ProjectCodeCaption)]
        [RegularExpression(ProjectCodeLimitation,
            ErrorMessage = ProjectCodeErrorMessage)]
        public string? ProjectCode { get; set; }

        [Display(Name = DescriptionCaption)]
        [StringLength(DescriptionMaxLength, MinimumLength = 0)]
        public string? Description { get; set; }

        [Display(Name = BudgetUsdCaption)]
        [Range(0, (double)decimal.MaxValue,
            ErrorMessage = NonNegativeMessage)]
        [DataType(DataType.Currency)]
        public decimal? Budget { get; set; }

        [Display(Name = ShipyardCaption)]
        [Required(ErrorMessage = RequiredSelectionMessage)]
        public string? ShipyardName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ShipyardSelectList { get; set; }
    }
}
