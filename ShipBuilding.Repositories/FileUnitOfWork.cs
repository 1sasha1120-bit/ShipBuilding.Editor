using ShipBuilding.Data;
using ShipBuilding.Repositories.Interfaces;

namespace ShipBuilding.Repositories
{
    public class FileUnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public FileUnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;

            TypeOfShipsRepository = new TypeOfShipsRepository(
                dataContext.DataSet.Types);
            ShipyardsRepository = new ShipyardsRepository(
                dataContext.DataSet.Shipyards);
            ShipProjectsRepository = new ShipProjectsRepository(
                dataContext.DataSet.ShipProjects);
            ShipsRepository = new ShipsRepository(
                dataContext.DataSet.Ships);
        }

        public ITypeOfShipsRepository TypeOfShipsRepository { get; }
        public IShipyardsRepository ShipyardsRepository { get; }
        public IShipsRepository ShipsRepository { get; }
        public IShipProjectsRepository ShipProjectsRepository { get; }

        public void Dispose()
        {
        }

        public void Save()
        {
            _dataContext.Save();
        }
    }
}
