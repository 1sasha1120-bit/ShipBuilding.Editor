using ShipBuilding.Statistics.Dashboard;
using ShipBuilding.Statistics.DataQuality;

namespace ShipBuilding.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторій лише для читання: зведення й обрахунки над даними предметної області.
    /// </summary>
    public interface IStatisticsRepository
    {
        DashboardModel GetDashboard();

        IReadOnlyDictionary<EntityKind, int> GetEntityCounts();

        IReadOnlyList<IncompleteRecord> GetIncompleteRecords();
    }
}
