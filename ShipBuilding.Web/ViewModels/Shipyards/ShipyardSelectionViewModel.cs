using Common.Types;
using ShipBuilding.Services.Shipyards;

namespace ShipBuilding.Web.ViewModels.Shipyards
{
    public class ShipyardSelectionViewModel
    {
        public ShipyardsFilter Filter { get; set; }
        public IEnumerable<ShipyardBrowsingViewModel> FilteredData { get; private set; }
        public ShipyardBrowsingViewModel SelectedObject { get; set; } = new();

        public string SortBy { get; }

        public SortingOrder Order { get; }

        public ShipyardSelectionViewModel(ShipyardsFilter filter, ShipyardSortField sortBy, SortingOrder order,
            IEnumerable<ShipyardBrowsingViewModel> data)
        {
            Filter = filter;
            SortBy = sortBy.ToString();
            Order = order;
            FilteredData = data.ToList();
        }

        public SortingOrder NextOrder(string column) =>
            column == SortBy && Order == SortingOrder.Ascending ? SortingOrder.Descending : SortingOrder.Ascending;
    }
}
