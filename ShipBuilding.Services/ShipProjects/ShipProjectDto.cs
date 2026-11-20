using ShipBuilding.Entities;

namespace ShipBuilding.Services.ShipProjects
{
    /// <summary>
    /// Дані проєкту: їх повертає сервіс і їх же він приймає для створення чи зміни.
    /// Верф задається назвою. Key заповнюється лише у відповідях сервісу.
    /// </summary>
    public record ShipProjectDto
    {
        public int Id { get; init; }
        public string Key { get; init; } = "";
        public string? Name { get; init; }
        public string? ProjectCode { get; init; }
        public string? Description { get; init; }
        public decimal? Budget { get; init; }
        public string? ShipyardName { get; init; }

        internal static ShipProjectDto From(ShipProject project)
        {
            return new()
            {
                Id           = project.Id,
                Key          = project.Key,
                Name         = project.Name,
                ProjectCode  = project.ProjectCode,
                Description  = project.Description,
                Budget       = project.Budget,
                ShipyardName = project.Shipyard?.Name
            };
        }
    }
}
