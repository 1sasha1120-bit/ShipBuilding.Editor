using Common.Repositories.Interfaces;

namespace ShipBuilding.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ITypeOfShipsRepository TypeOfShipsRepository { get; }
        IShipyardsRepository ShipyardsRepository { get; }
        IShipsRepository ShipsRepository { get; }
        IShipProjectsRepository ShipProjectsRepository { get; }

        void Save();
    }
}
