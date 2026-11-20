using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.Infrastructure;
using ShipBuilding.Services.Interfaces;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Services.Ships
{
    internal sealed class ShipsService : EntityService, IShipsService
    {
        public ShipsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private IShipsRepository Repository => UnitOfWork.ShipsRepository;

        // ---------------------------------------------------------------------------
        // Читання

        public ShipDto? GetById(int id)
        {
            return Repository.GetById(id) is Ship ship ? ShipDto.From(ship) : null;
        }

        public IReadOnlyList<ShipDto> GetList(ShipsQuery query)
        {
            var data = Repository.GetAll().Where(e =>
                Matches(e.Name, query.Name)
                && Matches(e.RegistrationNumber, query.RegistrationNumber)
                && Matches(e.Shipyard?.Name, query.ShipyardName)
                && Matches(e.TypeOfShip?.Name, query.TypeOfShipName));
            if (query.LaunchDateMin.HasValue)
                data = data.Where(e => e.LaunchDate >= query.LaunchDateMin);
            if (query.LaunchDateMax.HasValue)
                data = data.Where(e => e.LaunchDate <= query.LaunchDateMax);

            var sorted = query.SortBy switch
            {
                ShipSortField.RegistrationNumber => Sort(data, e => e.RegistrationNumber, query.Order),
                ShipSortField.LaunchDate         => Sort(data, e => e.LaunchDate, query.Order),
                ShipSortField.ShipyardName       => Sort(data, e => e.Shipyard?.Name, query.Order),
                ShipSortField.TypeOfShipName     => Sort(data, e => e.TypeOfShip?.Name, query.Order),
                _                                => Sort(data, e => e.Name, query.Order),
            };
            return sorted.ThenBy(e => e.Name).Select(ShipDto.From).ToList();
        }

        // ---------------------------------------------------------------------------
        // Запис

        public bool Create(ShipDto dto, out string? message)
        {
            if (!IntegrityChecking.CheckCreation(dto, UnitOfWork, out message))
                return false;

            var ship = ToEntity(dto);
            return TrySave(() => Repository.Add(ship), out message);
        }

        public bool Update(ShipDto dto, out string? message)
        {
            if (Repository.GetById(dto.Id) is null)
            {
                message = NotFoundMessage(ShipCaption, dto.Id);
                return false;
            }
            if (!IntegrityChecking.CheckUpdating(dto, UnitOfWork, out message))
                return false;

            var ship = ToEntity(dto);
            return TrySave(() => Repository.Update(ship), out message);
        }

        // У судна немає залежних записів, тож перевіряється лише його наявність
        public bool Delete(int id, out string? message)
        {
            if (Repository.GetById(id) is null)
            {
                message = NotFoundMessage(ShipCaption, id);
                return false;
            }
            return TrySave(() => Repository.Delete(id), out message);
        }

        // ---------------------------------------------------------------------------
        // Перетворення

        private Ship ToEntity(ShipDto dto)
        {
            return new(dto.Name, dto.RegistrationNumber, dto.LaunchDate, dto.Status, dto.Height, dto.Width, dto.Length,
                FindByName(UnitOfWork.ShipyardsRepository.GetAll(), dto.ShipyardName, e => e.Name),
                FindByName(UnitOfWork.TypeOfShipsRepository.GetAll(), dto.TypeOfShipName, e => e.Name),
                FindByName(UnitOfWork.ShipProjectsRepository.GetAll(), dto.ProjectName, e => e.Name))
            {
                Id = dto.Id
            };
        }
    }
}
