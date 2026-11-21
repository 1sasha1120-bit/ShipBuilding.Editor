namespace ShipBuilding.Web.ViewModels.TypeOfShips
{
    public class TypeOfShipSelectionViewModel
    {
        public TypeOfShipsFilter Filter { get; set; } = new();

        public IReadOnlyList<TypeOfShipBrowsingViewModel> TreeData { get; private set; } = [];

        public IEnumerable<TypeOfShipBrowsingViewModel> FilteredData { get; private set; } = [];

        public bool IsFiltered { get; private set; }

        public bool? ExpandAll { get; private set; }

        public TypeOfShipBrowsingViewModel SelectedObject { get; set; } = new();

        public TypeOfShipSelectionViewModel(
            TypeOfShipsFilter filter,
            IReadOnlyList<TypeOfShipBrowsingViewModel> treeData,
            IEnumerable<TypeOfShipBrowsingViewModel> flatData,
            bool isFiltered,
            bool? expandAll = null)
        {
            Filter       = filter;
            TreeData     = treeData;
            FilteredData = flatData;
            IsFiltered   = isFiltered;
            ExpandAll    = expandAll;
        }
    }
}
