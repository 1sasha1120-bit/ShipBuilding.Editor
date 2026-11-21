using Common.Types;
using ShipBuilding.Services.ShipProjects;

namespace ShipBuilding.Web.ViewModels.ShipProjects
{
    public class ShipProjectSelectionViewModel
    {
        public ShipProjectsFilter Filter { get; set; }
        public IEnumerable<ShipProjectBrowsingViewModel> FilteredData { get; private set; }
        public ShipProjectBrowsingViewModel SelectedObject { get; set; } = new();

        // Стовпець, за яким відсортовано список зараз.
        public string SortBy { get; }

        public SortingOrder Order { get; }

        public ShipProjectSelectionViewModel(ShipProjectsFilter filter, ShipProjectSortField sortBy, SortingOrder order,
            IEnumerable<ShipProjectBrowsingViewModel> data)
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
