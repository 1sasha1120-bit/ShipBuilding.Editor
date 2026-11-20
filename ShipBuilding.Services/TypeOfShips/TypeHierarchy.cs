using ShipBuilding.Entities;

namespace ShipBuilding.Services.TypeOfShips
{
    internal static class TypeHierarchy
    {
        /// <summary>
        /// Ідентифікатори всіх нащадків типу (без нього самого).
        /// Кожен вузол обходиться один раз, тому цикл у даних не зациклює обхід.
        /// </summary>
        public static HashSet<int> GetDescendantIds(IEnumerable<TypeOfShip> all, int rootId)
        {
            var types = all.ToList();
            var descendants = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(rootId);
            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                foreach (var child in types.Where(e => e.Parent?.Id == current))
                {
                    if (descendants.Add(child.Id))
                    {
                        queue.Enqueue(child.Id);
                    }
                }
            }
            return descendants;
        }

        /// <summary>
        /// Дерево типів: кореневі типи з підтипами на кожному рівні, упорядковані за назвою.
        /// Кожен тип потрапляє в дерево один раз, тому цикл у даних не зациклює побудову.
        /// </summary>
        public static IReadOnlyList<TypeOfShipDto> BuildTree(IEnumerable<TypeOfShip> all)
        {
            return BuildLevel(all.ToList(), parentId: null, depth: 0, visited: []);
        }

        /// <summary>Дерево пласким списком: батьківський тип, за ним його підтипи (без Children).</summary>
        public static IEnumerable<TypeOfShipDto> Flatten(IEnumerable<TypeOfShipDto> nodes)
        {
            foreach (var node in nodes)
            {
                yield return node with { Children = [] };
                foreach (var child in Flatten(node.Children))
                {
                    yield return child;
                }
            }
        }

        private static List<TypeOfShipDto> BuildLevel(List<TypeOfShip> types, int? parentId, int depth,
            HashSet<int> visited)
        {
            var level = new List<TypeOfShipDto>();
            foreach (var type in types.Where(e => e.Parent?.Id == parentId).OrderBy(e => e.Name))
            {
                if (!visited.Add(type.Id))
                {
                    continue;
                }
                level.Add(TypeOfShipDto.From(type) with
                {
                    Depth = depth,
                    Children = BuildLevel(types, type.Id, depth + 1, visited)
                });
            }
            return level;
        }
    }
}
