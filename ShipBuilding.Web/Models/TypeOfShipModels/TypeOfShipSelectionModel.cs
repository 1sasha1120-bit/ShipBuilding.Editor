namespace ShipBuilding.Web.Models.TypeOfShipModels
{
    public class TypeOfShipSelectionModel
    {
        public TypeOfShipsFilter Filter { get; set; } = new();

        public IReadOnlyList<TypeOfShipBrowsingModel> TreeData { get; private set; } = [];

        public IEnumerable<TypeOfShipBrowsingModel> FilteredData { get; private set; } = [];

        public bool IsFiltered { get; private set; }

        public bool? ExpandAll { get; private set; }

        public TypeOfShipBrowsingModel SelectedObject { get; set; } = new();

        public TypeOfShipSelectionModel(
            TypeOfShipsFilter filter,
            IReadOnlyList<TypeOfShipBrowsingModel> treeData,
            IEnumerable<TypeOfShipBrowsingModel> flatData,
            bool? expandAll = null)
        {
            Filter    = filter;
            TreeData  = treeData;
            ExpandAll = expandAll;

            IsFiltered = !string.IsNullOrWhiteSpace(filter.Name)
                      || !string.IsNullOrWhiteSpace(filter.TypeCode);

            FilteredData = IsFiltered ? ApplyFilter(flatData, filter) : flatData;
        }

        private static IEnumerable<TypeOfShipBrowsingModel> ApplyFilter(
            IEnumerable<TypeOfShipBrowsingModel> data, TypeOfShipsFilter f)
        {
            if (!string.IsNullOrWhiteSpace(f.Name))
                data = data.Where(e =>
                    e.Name?.Contains(f.Name.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            if (!string.IsNullOrWhiteSpace(f.TypeCode))
                data = data.Where(e =>
                    e.TypeCode?.Contains(f.TypeCode.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            return data;
        }
    }
}
