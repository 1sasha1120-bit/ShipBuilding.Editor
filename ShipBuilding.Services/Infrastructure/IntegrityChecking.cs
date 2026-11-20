using Common.Entities;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.ShipProjects;
using ShipBuilding.Services.Ships;
using ShipBuilding.Services.Shipyards;
using ShipBuilding.Services.TypeOfShips;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Services.Infrastructure
{
    // Перевірки цілісності даних перед створенням, зміною та видаленням записів.
    // Кожен метод повертає true, якщо зміна допустима; інакше в message — пояснення.
    // Те, що сам запис існує, перевіряє сервіс до виклику цих методів.
    internal static class IntegrityChecking
    {
        // ---------------------------------------------------------------------------
        // Верфі

        public static bool CheckCreation(ShipyardDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckNameUnique(unitOfWork.ShipyardsRepository.GetAll(), e => e.Name, dto.Name,
                ShipyardCaption, excludeId: null, out message);
        }

        public static bool CheckUpdating(ShipyardDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckNameUnique(unitOfWork.ShipyardsRepository.GetAll(), e => e.Name, dto.Name,
                ShipyardCaption, excludeId: dto.Id, out message);
        }

        // Верф не можна видалити, поки на неї посилаються проєкти чи судна
        public static bool CheckDeleting(Shipyard shipyard, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckNoDependents(ShipyardCaption, shipyard.Key, out message,
                (unitOfWork.ShipProjectsRepository.GetAll().Any(e => e.Shipyard?.Key == shipyard.Key), ProjectsHeader),
                (unitOfWork.ShipsRepository.GetAll().Any(e => e.Shipyard?.Key == shipyard.Key), ShipsHeader));
        }

        // ---------------------------------------------------------------------------
        // Проєкти

        public static bool CheckCreation(ShipProjectDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckReference(unitOfWork.ShipyardsRepository.GetAll(), e => e.Name, dto.ShipyardName,
                    ShipyardCaption, out message)
                && CheckNameUnique(unitOfWork.ShipProjectsRepository.GetAll(), e => e.Name, dto.Name,
                    ProjectCaption, excludeId: null, out message);
        }

        public static bool CheckUpdating(ShipProjectDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckReference(unitOfWork.ShipyardsRepository.GetAll(), e => e.Name, dto.ShipyardName,
                    ShipyardCaption, out message)
                && CheckNameUnique(unitOfWork.ShipProjectsRepository.GetAll(), e => e.Name, dto.Name,
                    ProjectCaption, excludeId: dto.Id, out message);
        }

        // Проєкт не можна видалити, поки за ним є судна
        public static bool CheckDeleting(ShipProject project, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckNoDependents(ProjectCaption, project.Key, out message,
                (unitOfWork.ShipsRepository.GetAll().Any(e => e.Project?.Key == project.Key), ShipsHeader));
        }

        // ---------------------------------------------------------------------------
        // Типи суден

        public static bool CheckCreation(TypeOfShipDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckReference(unitOfWork.TypeOfShipsRepository.GetAll(), e => e.Name, dto.ParentName,
                    TypeOfShipCaption, out message)
                && CheckNameUnique(unitOfWork.TypeOfShipsRepository.GetAll(), e => e.Name, dto.Name,
                    TypeOfShipCaption, excludeId: null, out message);
        }

        public static bool CheckUpdating(TypeOfShipDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckReference(unitOfWork.TypeOfShipsRepository.GetAll(), e => e.Name, dto.ParentName,
                    TypeOfShipCaption, out message)
                && CheckNameUnique(unitOfWork.TypeOfShipsRepository.GetAll(), e => e.Name, dto.Name,
                    TypeOfShipCaption, excludeId: dto.Id, out message)
                && CheckNoCycle(dto, unitOfWork, out message);
        }

        // Тип не можна видалити, поки в нього є підтипи чи судна цього типу
        public static bool CheckDeleting(TypeOfShip type, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckNoDependents(TypeOfShipCaption, type.Key, out message,
                (unitOfWork.TypeOfShipsRepository.GetAll().Any(e => e.Parent?.Key == type.Key), TypeOfShipsHeader),
                (unitOfWork.ShipsRepository.GetAll().Any(e => e.TypeOfShip?.Key == type.Key), ShipsHeader));
        }

        // Батьком не може бути сам тип або його нащадок — інакше в ієрархії утвориться цикл
        private static bool CheckNoCycle(TypeOfShipDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            var types = unitOfWork.TypeOfShipsRepository.GetAll();
            message = null;
            if (EntityService.FindByName(types, dto.ParentName, e => e.Name) is TypeOfShip parent
                && (parent.Id == dto.Id || TypeHierarchy.GetDescendantIds(types, dto.Id).Contains(parent.Id)))
            {
                message = HierarchyCycleMessage(TypeOfShipCaption);
            }
            return message is null;
        }

        // ---------------------------------------------------------------------------
        // Судна

        public static bool CheckCreation(ShipDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckShipReferences(dto, unitOfWork, out message)
                && CheckShipUnique(dto, unitOfWork, excludeId: null, out message);
        }

        public static bool CheckUpdating(ShipDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckShipReferences(dto, unitOfWork, out message)
                && CheckShipUnique(dto, unitOfWork, excludeId: dto.Id, out message);
        }

        // Верф, тип і проєкт, якщо задані назвами, мають існувати
        private static bool CheckShipReferences(ShipDto dto, IUnitOfWork unitOfWork, out string? message)
        {
            return CheckReference(unitOfWork.ShipyardsRepository.GetAll(), e => e.Name, dto.ShipyardName,
                    ShipyardCaption, out message)
                && CheckReference(unitOfWork.TypeOfShipsRepository.GetAll(), e => e.Name, dto.TypeOfShipName,
                    TypeOfShipCaption, out message)
                && CheckReference(unitOfWork.ShipProjectsRepository.GetAll(), e => e.Name, dto.ProjectName,
                    ProjectCaption, out message);
        }

        // Назва й реєстраційний номер судна мають бути унікальними
        private static bool CheckShipUnique(ShipDto dto, IUnitOfWork unitOfWork, int? excludeId, out string? message)
        {
            var ships = unitOfWork.ShipsRepository.GetAll();
            var byName = FindDuplicate(ships, e => e.Name, dto.Name, excludeId);
            var byNumber = FindDuplicate(ships, e => e.RegistrationNumber, dto.RegistrationNumber, excludeId);

            message = byName is null ? null : DuplicateNameMessage(ShipCaption, byName.Name);
            if (byNumber is not null)
            {
                message = message is null
                    ? DuplicateRegistrationNumberMessage(ShipCaption, byNumber.RegistrationNumber)
                    : message + AndRegistrationNumberMessage(byNumber.RegistrationNumber);
            }
            return message is null;
        }

        // ---------------------------------------------------------------------------
        // Базові перевірки

        // Назва запису має бути унікальною серед записів цієї сутності
        // (excludeId — сам запис, що змінюється; null під час створення)
        private static bool CheckNameUnique<T>(IEnumerable<T> all, Func<T, string?> nameOf, string? name,
            string entityCaption, int? excludeId, out string? message)
            where T : Entity
        {
            var duplicate = FindDuplicate(all, nameOf, name, excludeId);
            message = duplicate is null ? null : DuplicateNameMessage(entityCaption, nameOf(duplicate));
            return duplicate is null;
        }

        // Якщо пов'язаний запис задано назвою, він має існувати; порожня назва — посилання не задане
        private static bool CheckReference<T>(IEnumerable<T> all, Func<T, string?> nameOf, string? name,
            string entityCaption, out string? message)
            where T : class
        {
            message = null;
            if (!string.IsNullOrWhiteSpace(name) && EntityService.FindByName(all, name, nameOf) is null)
            {
                message = ReferenceNotFoundMessage(entityCaption, name.Trim());
            }
            return message is null;
        }

        // Видаляти можна лише запис без залежних. Повідомлення: перший вид залежних — повним реченням,
        // наступні — доповненням «та …»
        private static bool CheckNoDependents(string entityCaption, string? key, out string? message,
            params (bool Exist, string Header)[] dependents)
        {
            message = null;
            foreach (var (exist, header) in dependents)
            {
                if (exist)
                {
                    message = message is null
                        ? DeletingForbiddenMessage(entityCaption, key, header)
                        : message + AndDependentMessage(header);
                }
            }
            return message is null;
        }

        // Інший запис із таким самим значенням поля (без урахування регістру)
        private static T? FindDuplicate<T>(IEnumerable<T> all, Func<T, string?> field, string? value,
            int? excludeId)
            where T : Entity
        {
            return string.IsNullOrEmpty(value)
                ? null
                : all.FirstOrDefault(e => e.Id != excludeId && string.Compare(field(e), value, true) == 0);
        }
    }
}
