using Common.Types;

namespace ShipBuilding.Services.ShipProjects
{
    public enum ShipProjectSortField
    {
        Name, ProjectCode, ShipyardName, Budget
    }

    /// <summary>
    /// Критерії пошуку проєктів. Незадані критерії не обмежують вибірку.
    /// </summary>
    public record ShipProjectsQuery
    {
        public string? Name { get; init; }
        public string? ProjectCode { get; init; }
        public string? ShipyardName { get; init; }

        public ShipProjectSortField SortBy { get; init; } = ShipProjectSortField.Name;
        public SortingOrder Order { get; init; } = SortingOrder.Ascending;
    }
}
