using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.Infrastructure;
using ShipBuilding.Services.Interfaces;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Services.TypeOfShips
{
    internal sealed class TypeOfShipsService : EntityService, ITypeOfShipsService
    {
        public TypeOfShipsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private ITypeOfShipsRepository Repository => UnitOfWork.TypeOfShipsRepository;

        // ---------------------------------------------------------------------------
        // Читання

        public TypeOfShipDto? GetById(int id)
        {
            return Repository.GetById(id) is TypeOfShip type ? TypeOfShipDto.From(type) : null;
        }

        public IReadOnlyList<TypeOfShipDto> GetList(TypeOfShipsQuery query)
        {
            return TypeHierarchy.Flatten(GetTree())
                .Where(e => Matches(e.Name, query.Name) && Matches(e.TypeCode, query.TypeCode))
                .ToList();
        }

        public IReadOnlyList<TypeOfShipDto> GetTree()
        {
            return TypeHierarchy.BuildTree(Repository.GetAll());
        }

        public IReadOnlyList<string> GetNames()
        {
            return SortedNames(Repository.GetAll().Select(e => e.Name));
        }

        public IReadOnlyList<string> GetParentCandidates(int? excludeId = null)
        {
            IEnumerable<TypeOfShip> all = Repository.GetAll();
            if (excludeId is int id)
            {
                var excluded = TypeHierarchy.GetDescendantIds(all, id);
                excluded.Add(id);
                all = all.Where(e => !excluded.Contains(e.Id));
            }
            return SortedNames(all.Select(e => e.Name));
        }

        // ---------------------------------------------------------------------------
        // Запис

        public bool Create(TypeOfShipDto dto, out string? message)
        {
            if (!IntegrityChecking.CheckCreation(dto, UnitOfWork, out message))
                return false;

            var type = ToEntity(dto);
            return TrySave(() => Repository.Add(type), out message);
        }

        public bool Update(TypeOfShipDto dto, out string? message)
        {
            if (Repository.GetById(dto.Id) is null)
            {
                message = NotFoundMessage(TypeOfShipCaption, dto.Id);
                return false;
            }
            if (!IntegrityChecking.CheckUpdating(dto, UnitOfWork, out message))
                return false;

            var type = ToEntity(dto);
            return TrySave(() => Repository.Update(type), out message);
        }

        public bool Delete(int id, out string? message)
        {
            if (Repository.GetById(id) is not TypeOfShip type)
            {
                message = NotFoundMessage(TypeOfShipCaption, id);
                return false;
            }
            if (!IntegrityChecking.CheckDeleting(type, UnitOfWork, out message))
                return false;

            return TrySave(() => Repository.Delete(id), out message);
        }

        // ---------------------------------------------------------------------------
        // Перетворення

        private TypeOfShip ToEntity(TypeOfShipDto dto)
        {
            return new(dto.Name, FindByName(Repository.GetAll(), dto.ParentName, e => e.Name), dto.TypeCode)
            {
                Id          = dto.Id,
                Description = dto.Description
            };
        }
    }
}
