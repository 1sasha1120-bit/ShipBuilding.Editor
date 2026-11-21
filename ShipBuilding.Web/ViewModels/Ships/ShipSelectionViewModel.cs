using Common.Types;
using ShipBuilding.Services.Ships;

namespace ShipBuilding.Web.ViewModels.Ships
{
    public class ShipSelectionViewModel
    {
        public ShipSelectionViewModel(ShipsFilter filter, ShipSortField sortBy, SortingOrder order,
            IEnumerable<ShipBrowsingViewModel> data)
        {
            Filter = filter;
            SortBy = sortBy.ToString();
            Order = order;
            FilteredData = data.ToList();
        }

        public ShipsFilter Filter { get; set; }
        public IEnumerable<ShipBrowsingViewModel> FilteredData { get; private set; }

        public string SortBy { get; }

        public SortingOrder Order { get; }

        public SortingOrder NextOrder(string column) =>
            column == SortBy && Order == SortingOrder.Ascending ? SortingOrder.Descending : SortingOrder.Ascending;
    }
}
