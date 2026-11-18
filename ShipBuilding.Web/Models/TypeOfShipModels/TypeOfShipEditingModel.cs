
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.TypeOfShipModels
{
    public class TypeOfShipEditingModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        [Required(ErrorMessage = $"Потрібно заповнити поле '{NameCaption}'")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = $"Назва повинна містити від {{2}} до {{1}} символів")]
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

        public static explicit operator TypeOfShipEditingModel(TypeOfShip obj) =>
            new TypeOfShipEditingModel
            {
                Id          = obj.Id,
                Name        = obj.Name,
                TypeCode    = obj.TypeCode,
                ParentName  = obj.Parent?.Name,
                Description = obj.Description
            };
    }
}

