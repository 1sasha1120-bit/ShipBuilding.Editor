using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.Infrastructure;
using ShipBuilding.Services.Interfaces;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Services.ShipProjects
{
    internal sealed class ShipProjectsService : EntityService, IShipProjectsService
    {
        public ShipProjectsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private IShipProjectsRepository Repository => UnitOfWork.ShipProjectsRepository;

        // ---------------------------------------------------------------------------
        // Читання

        public ShipProjectDto? GetById(int id)
        {
            return Repository.GetById(id) is ShipProject project ? ShipProjectDto.From(project) : null;
        }

        public IReadOnlyList<ShipProjectDto> GetList(ShipProjectsQuery query)
        {
            var data = Repository.GetAll().Where(e =>
                Matches(e.Name, query.Name)
                && Matches(e.ProjectCode, query.ProjectCode)
                && Matches(e.Shipyard?.Name, query.ShipyardName));

            var sorted = query.SortBy switch
            {
                ShipProjectSortField.ProjectCode  => Sort(data, e => e.ProjectCode, query.Order),
                ShipProjectSortField.ShipyardName => Sort(data, e => e.Shipyard?.Name, query.Order),
                ShipProjectSortField.Budget       => Sort(data, e => e.Budget, query.Order),
                _                                 => Sort(data, e => e.Name, query.Order),
            };
            return sorted.ThenBy(e => e.Name).Select(ShipProjectDto.From).ToList();
        }

        public IReadOnlyList<string> GetNames()
        {
            return SortedNames(Repository.GetAll().Select(e => e.Name));
        }

        // ---------------------------------------------------------------------------
        // Запис

        public bool Create(ShipProjectDto dto, out string? message)
        {
            if (!IntegrityChecking.CheckCreation(dto, UnitOfWork, out message))
                return false;

            var project = ToEntity(dto);
            return TrySave(() => Repository.Add(project), out message);
        }

        public bool Update(ShipProjectDto dto, out string? message)
        {
            if (Repository.GetById(dto.Id) is null)
            {
                message = NotFoundMessage(ProjectCaption, dto.Id);
                return false;
            }
            if (!IntegrityChecking.CheckUpdating(dto, UnitOfWork, out message))
                return false;

            var project = ToEntity(dto);
            return TrySave(() => Repository.Update(project), out message);
        }

        public bool Delete(int id, out string? message)
        {
            if (Repository.GetById(id) is not ShipProject project)
            {
                message = NotFoundMessage(ProjectCaption, id);
                return false;
            }
            if (!IntegrityChecking.CheckDeleting(project, UnitOfWork, out message))
                return false;

            return TrySave(() => Repository.Delete(id), out message);
        }

        // ---------------------------------------------------------------------------
        // Перетворення

        private ShipProject ToEntity(ShipProjectDto dto)
        {
            return new(dto.Name, dto.ProjectCode, dto.Budget,
                FindByName(UnitOfWork.ShipyardsRepository.GetAll(), dto.ShipyardName, e => e.Name))
            {
                Id          = dto.Id,
                Description = dto.Description
            };
        }
    }
}
