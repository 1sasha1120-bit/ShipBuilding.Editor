using ShipBuilding.Analysis.Extensions;
using ShipBuilding.Data;
using ShipBuilding.Data.Interfaces;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Statistics.Dashboard;
using ShipBuilding.Statistics.DataQuality;

namespace ShipBuilding.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DashboardModel GetDashboard() => Snapshot().GetDashboardModel();

        public IReadOnlyDictionary<EntityKind, int> GetEntityCounts() => Snapshot().GetEntityCounts();

        public IReadOnlyList<IncompleteRecord> GetIncompleteRecords() => Snapshot().GetIncompleteRecords();

        // Знімок поточних даних, щоб усі обрахунки в межах одного виклику бачили той самий стан
        private IReadOnlyDataSet Snapshot() => new ReadOnlyDataSet
        {
            Types = _unitOfWork.TypeOfShipsRepository.GetAll().ToList(),
            Shipyards = _unitOfWork.ShipyardsRepository.GetAll().ToList(),
            ShipProjects = _unitOfWork.ShipProjectsRepository.GetAll().ToList(),
            Ships = _unitOfWork.ShipsRepository.GetAll().ToList()
        };
    }
}
