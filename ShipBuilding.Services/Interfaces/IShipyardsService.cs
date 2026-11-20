using ShipBuilding.Services.Shipyards;

namespace ShipBuilding.Services.Interfaces
{
    /// <summary>Операції над верфями; видаляється верф, лише якщо на неї не посилаються проєкти й судна.</summary>
    public interface IShipyardsService : IEntityService<ShipyardDto, ShipyardsQuery>
    {
        /// <summary>Назви верфей за абеткою — для випадних списків.</summary>
        IReadOnlyList<string> GetNames();
    }
}
