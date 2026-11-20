using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.Infrastructure;
using ShipBuilding.Services.Interfaces;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Services.Shipyards
{
    internal sealed class ShipyardsService : EntityService, IShipyardsService
    {
        public ShipyardsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private IShipyardsRepository Repository => UnitOfWork.ShipyardsRepository;

        // ---------------------------------------------------------------------------
        // Читання

        public ShipyardDto? GetById(int id)
        {
            return Repository.GetById(id) is Shipyard shipyard ? ShipyardDto.From(shipyard) : null;
        }

        public IReadOnlyList<ShipyardDto> GetList(ShipyardsQuery query)
        {
            var data = Repository.GetAll().Where(e =>
                Matches(e.Name, query.Name)
                && Matches(e.CountryName, query.CountryName));
            if (query.YearOfFoundedFrom.HasValue)
                data = data.Where(e => e.YearOfFounded >= query.YearOfFoundedFrom);
            if (query.YearOfFoundedTo.HasValue)
                data = data.Where(e => e.YearOfFounded <= query.YearOfFoundedTo);

            var sorted = query.SortBy switch
            {
                ShipyardSortField.CountryName     => Sort(data, e => e.CountryName, query.Order),
                ShipyardSortField.YearOfFounded   => Sort(data, e => e.YearOfFounded, query.Order),
                ShipyardSortField.NumberOfDocks   => Sort(data, e => e.NumberOfDocks, query.Order),
                ShipyardSortField.CapacityPerYear => Sort(data, e => e.CapacityPerYear, query.Order),
                _                                 => Sort(data, e => e.Name, query.Order),
            };
            return sorted.ThenBy(e => e.Name).Select(ShipyardDto.From).ToList();
        }

        public IReadOnlyList<string> GetNames()
        {
            return SortedNames(Repository.GetAll().Select(e => e.Name));
        }

        // ---------------------------------------------------------------------------
        // Запис

        public bool Create(ShipyardDto dto, out string? message)
        {
            if (!IntegrityChecking.CheckCreation(dto, UnitOfWork, out message))
                return false;

            var shipyard = ToEntity(dto);
            return TrySave(() => Repository.Add(shipyard), out message);
        }

        public bool Update(ShipyardDto dto, out string? message)
        {
            if (Repository.GetById(dto.Id) is null)
            {
                message = NotFoundMessage(ShipyardCaption, dto.Id);
                return false;
            }
            if (!IntegrityChecking.CheckUpdating(dto, UnitOfWork, out message))
                return false;

            var shipyard = ToEntity(dto);
            return TrySave(() => Repository.Update(shipyard), out message);
        }

        public bool Delete(int id, out string? message)
        {
            if (Repository.GetById(id) is not Shipyard shipyard)
            {
                message = NotFoundMessage(ShipyardCaption, id);
                return false;
            }
            if (!IntegrityChecking.CheckDeleting(shipyard, UnitOfWork, out message))
                return false;

            return TrySave(() => Repository.Delete(id), out message);
        }

        // ---------------------------------------------------------------------------
        // Перетворення

        private static Shipyard ToEntity(ShipyardDto dto)
        {
            return new(dto.Name, dto.CountryName, dto.YearOfFounded, dto.NumberOfDocks, dto.CapacityPerYear)
            {
                Id = dto.Id
            };
        }
    }
}
