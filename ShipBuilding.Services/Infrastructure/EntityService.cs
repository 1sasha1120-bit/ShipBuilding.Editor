using Common.Types;
using ShipBuilding.Repositories.Interfaces;

namespace ShipBuilding.Services.Infrastructure
{
    internal abstract class EntityService
    {
        protected EntityService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        // ---------------------------------------------------------------------------
        // Збереження

        protected bool TrySave(Action change, out string? message)
        {
            try
            {
                change();
                UnitOfWork.Save();
                message = null;
                return true;
            }
            catch (Exception ex)
            {
                message = $"Помилка збереження: {ex.Message}";
                return false;
            }
        }

        // ---------------------------------------------------------------------------
        // Пошук, відбір і сортування

        // Запис із такою назвою (без урахування регістру) або null; потрібен і IntegrityChecking
        internal static T? FindByName<T>(IEnumerable<T> all, string? name, Func<T, string?> nameOf)
            where T : class
        {
            return string.IsNullOrWhiteSpace(name)
                ? null
                : all.FirstOrDefault(e => string.Equals(nameOf(e), name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        protected static bool Matches(string? value, string? part)
        {
            return string.IsNullOrWhiteSpace(part)
                || value?.Contains(part.Trim(), StringComparison.OrdinalIgnoreCase) == true;
        }

        protected static IOrderedEnumerable<T> Sort<T, TKey>(IEnumerable<T> source,
            Func<T, TKey> key, SortingOrder order)
        {
            return order == SortingOrder.Ascending ? source.OrderBy(key) : source.OrderByDescending(key);
        }

        protected static IReadOnlyList<string> SortedNames(IEnumerable<string?> names)
        {
            return names.OfType<string>().OrderBy(e => e).ToList();
        }
    }
}
