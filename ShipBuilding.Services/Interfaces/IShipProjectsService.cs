using ShipBuilding.Services.ShipProjects;

namespace ShipBuilding.Services.Interfaces
{
    /// <summary>Операції над проєктами; видаляється проєкт, лише якщо за ним немає суден.</summary>
    public interface IShipProjectsService : IEntityService<ShipProjectDto, ShipProjectsQuery>
    {
        /// <summary>Назви проєктів за абеткою — для випадних списків.</summary>
        IReadOnlyList<string> GetNames();
    }
}
