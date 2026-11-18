using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipProjectModels
{
    public class ShipProjectEditingModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        [Required(ErrorMessage = $"Потрібно заповнити поле '{NameCaption}'")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = $"Назва повинна містити від {{2}} до {{1}} символів")]
        public string? Name { get; set; }

        [Display(Name = "Код проєкту")]
        [RegularExpression(ProjectCodeLimitation,
            ErrorMessage = "Формат: SP-РРРР-НН (напр. SP-2024-001)")]
        public string? ProjectCode { get; set; }

        [Display(Name = DescriptionCaption)]
        [StringLength(DescriptionMaxLength, MinimumLength = 0)]
        public string? Description { get; set; }

        [Display(Name = "Бюджет, USD")]
        [Range(0, (double)decimal.MaxValue,
            ErrorMessage = "Бюджет не може бути від'ємним")]
        [DataType(DataType.Currency)]
        public decimal? Budget { get; set; }

        [Display(Name = ShipyardCaption)]
        [Required(ErrorMessage = $"Потрібно обрати '{ShipyardCaption}'")]
        public string? ShipyardName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ShipyardSelectList { get; set; }

        public static explicit operator ShipProjectEditingModel(ShipProject obj) =>
            new ShipProjectEditingModel
            {
                Id = obj.Id,
                Name = obj.Name,
                ProjectCode = obj.ProjectCode,
                Description = obj.Description,
                Budget = obj.Budget,
                ShipyardName = obj.Shipyard?.Name
            };
    }
}
