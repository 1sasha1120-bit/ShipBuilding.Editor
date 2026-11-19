using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;

namespace ShipBuilding.Web.Models.Extensions
{
    public static class SelectListMethods
    {
        public static SelectList CreateShipyardSelectList(this IUnitOfWork unitOfWork,
            string? selectedValue = "") =>
            CreateNameSelectList(unitOfWork.ShipyardsRepository.GetAll().Select(e => e.Name),
                selectedValue);

        public static SelectList CreateTypeOfShipSelectList(this IUnitOfWork unitOfWork,
            string? selectedValue = "") =>
            CreateNameSelectList(unitOfWork.TypeOfShipsRepository.GetAll().Select(e => e.Name),
                selectedValue);

        public static SelectList CreateProjectSelectList(this IUnitOfWork unitOfWork,
            string? selectedValue = "") =>
            CreateNameSelectList(unitOfWork.ShipProjectsRepository.GetAll().Select(e => e.Name),
                selectedValue);

        /// <summary>
        /// Список можливих батьківських типів суден. Якщо задано excludeId,
        /// з нього виключаються цей тип та всі його нащадки (щоб уникнути циклів).
        /// </summary>
        public static SelectList CreateParentSelectList(this IEnumerable<TypeOfShip> typesOfShips,
            string? selectedValue = "", int? excludeId = null)
        {
            IEnumerable<TypeOfShip> candidates = typesOfShips;

            if (excludeId.HasValue)
            {
                var excluded = new HashSet<int>();
                var queue = new Queue<int>();
                queue.Enqueue(excludeId.Value);
                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    excluded.Add(current);
                    foreach (var child in typesOfShips.Where(e => e.Parent?.Id == current))
                        queue.Enqueue(child.Id);
                }
                candidates = candidates.Where(e => !excluded.Contains(e.Id));
            }

            return CreateNameSelectList(candidates.Select(e => e.Name), selectedValue);
        }

        private static SelectList CreateNameSelectList(IEnumerable<string?> names, string? selectedValue) =>
            new(names.OrderBy(e => e), selectedValue);
    }
}
