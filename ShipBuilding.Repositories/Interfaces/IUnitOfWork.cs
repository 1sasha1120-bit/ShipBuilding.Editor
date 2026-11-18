using Common.Repositories.Interfaces;

namespace ShipBuilding.Repositories.Interfaces
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        ITypeOfShipsRepository TypeOfShipsRepository { get; }
        IShipyardsRepository ShipyardsRepository { get; }
        IShipsRepository ShipsRepository { get; }
        IShipProjectsRepository ShipProjectsRepository { get; }
    }
}
