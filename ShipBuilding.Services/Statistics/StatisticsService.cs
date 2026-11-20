using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.ShipProjects;
using ShipBuilding.Services.Ships;
using ShipBuilding.Types;

namespace ShipBuilding.Services.Statistics
{
    internal sealed class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DashboardModel GetDashboard()
        {
            // Знімок поточних даних, щоб усі обрахунки бачили той самий стан
            var types = _unitOfWork.TypeOfShipsRepository.GetAll().ToList();
            var shipyards = _unitOfWork.ShipyardsRepository.GetAll().ToList();
            var projects = _unitOfWork.ShipProjectsRepository.GetAll().ToList();
            var ships = _unitOfWork.ShipsRepository.GetAll().ToList();

            var withBudget = projects.Where(p => p.Budget.HasValue).ToList();

            var model = new DashboardModel();

            // Кількість записів
            model.TypesCount = types.Count;
            model.ShipyardsCount = shipyards.Count;
            model.ProjectsCount = projects.Count;
            model.ShipsCount = ships.Count;

            // Розподіл суден за статусом
            model.ShipsByStatus = ships
                .GroupBy(e => e.Status)
                .OrderBy(g => g.Key == ShipStatus.Unknown ? int.MaxValue : (int)g.Key)
                .Select(g => new DashboardModel.StatusShare(g.Key, g.Count(),
                    100.0 * g.Count() / ships.Count))
                .ToList();

            // Найбільші верфі
            model.Shipyards = shipyards
                .Select(y => new DashboardModel.ShipyardSummary(y.Id, y.Name, y.CountryName,
                    ships.Count(s => ReferenceEquals(s.Shipyard, y)),
                    projects.Count(p => ReferenceEquals(p.Shipyard, y))))
                .OrderByDescending(y => y.ShipsCount)
                .ThenBy(y => y.Name)
                .Take(5)
                .ToList();

            // Останні спущені на воду судна
            model.LatestShips = ships
                .Where(e => e.LaunchDate.HasValue)
                .OrderByDescending(e => e.LaunchDate)
                .Take(5)
                .Select(ShipDto.From)
                .ToList();

            // Бюджет проєктів
            model.TotalBudget = withBudget.Sum(p => p.Budget!.Value);
            model.AverageBudget = withBudget.Count == 0 ? 0 : withBudget.Average(p => p.Budget!.Value);
            model.ProjectsWithBudget = withBudget.Count;

            var top = withBudget.MaxBy(p => p.Budget);
            model.MostExpensiveProject = top != null ? ShipProjectDto.From(top) : null;

            // Проєкти, за якими ще не збудовано жодного судна
            model.ProjectsWithoutShips = projects
                .Where(p => !ships.Any(s => s.Project?.Id == p.Id))
                .OrderBy(p => p.Name)
                .Select(ShipProjectDto.From)
                .ToList();

            return model;
        }
    }
}
