using ShipBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebTexts;

namespace ShipBuilding.Web.Models.ShipProjectModels
{
    public class ShipProjectBrowsingModel
    {
        public int Id { get; set; }

        [Display(Name = NameCaption)]
        public string? Name { get; set; }

        [Display(Name = ProjectCodeCaption)]
        public string? ProjectCode { get; set; }

        [Display(Name = ShipyardCaption)]
        public string? ShipyardName { get; set; }

        [Display(Name = BudgetUsdCaption)]
        [DataType(DataType.Currency)]
        public decimal? Budget { get; set; }

        [Display(Name = DescriptionCaption)]
        public string? Description { get; set; }

        public static explicit operator ShipProjectBrowsingModel(ShipProject obj) =>
            new ShipProjectBrowsingModel
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
