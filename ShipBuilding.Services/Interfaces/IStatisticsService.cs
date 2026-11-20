using ShipBuilding.Services.Statistics;

namespace ShipBuilding.Services.Interfaces
{
    public interface IStatisticsService
    {
        DashboardModel GetDashboard();
    }
}
