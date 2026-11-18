using ShipBuilding.Entities;
using ShipBuilding.Web.Models.TypeOfShipModels;

namespace ShipBuilding.Web.Infrastructure
{
    public static class TreeBuilder
    {
        public static List<TypeOfShipBrowsingModel> BuildTree(IEnumerable<TypeOfShip> all)
        {
            var list = all.ToList();
            var roots = new List<TypeOfShipBrowsingModel>();
            BuildTreeRecursive(list, roots, null, 0, new HashSet<int>());
            return roots;
        }

        private static void BuildTreeRecursive(
            List<TypeOfShip> all,
            List<TypeOfShipBrowsingModel> target,
            int? parentId,
            int depth,
            HashSet<int> visited)
        {
            var children = all
                .Where(e => parentId == null ? e.Parent == null : e.Parent?.Id == parentId)
                .OrderBy(e => e.Name);
                //.ToList();

            foreach (var node in children)
            {
                if (!visited.Add(node.Id)) continue; 

                var model = new TypeOfShipBrowsingModel
                {
                    Id          = node.Id,
                    Name        = node.Name,
                    TypeCode    = node.TypeCode,
                    ParentName  = node.Parent?.Name,
                    //Description = node.Description,
                    Depth       = depth,
                    Children    = new List<TypeOfShipBrowsingModel>()
                };
                BuildTreeRecursive(all, model.Children, node.Id, depth + 1, visited);
                target.Add(model);
            }
        }

        public static List<TypeOfShipBrowsingModel> BuildFlatList(IEnumerable<TypeOfShip> all)
        {
            var list = all.ToList();
            var result = new List<TypeOfShipBrowsingModel>();
            BuildFlatRecursive(list, result, null, 0);
            return result;
        }

        private static void BuildFlatRecursive(
            List<TypeOfShip> all,
            List<TypeOfShipBrowsingModel> result,
            int? parentId,
            int depth)
        {
            var children = all
                .Where(e => depth == 0 ? e.Parent == null : e.Parent?.Id == parentId)
                .OrderBy(e => e.Name);
                //.ToList();

            foreach (var node in children)
            {
                result.Add(new TypeOfShipBrowsingModel
                {
                    Id          = node.Id,
                    Name        = node.Name,
                    TypeCode    = node.TypeCode,
                    ParentName  = node.Parent?.Name,
                    Depth       = depth
                });
                BuildFlatRecursive(all, result, node.Id, depth + 1);
            }
        }
    }
}
