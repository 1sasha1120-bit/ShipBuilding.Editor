using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.DashboardModels
{
    public record BudgetSummary(
        [property: Display(Name = TotalBudgetCaption)] decimal Total,
        [property: Display(Name = AverageBudgetCaption)] decimal Average,
        [property: Display(Name = ProjectsWithBudgetCaption)] int ProjectsWithBudget,
        [property: Display(Name = MostExpensiveProjectCaption)] ProjectInfo? MostExpensive);
}
