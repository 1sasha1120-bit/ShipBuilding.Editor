using Common.Types;
using ShipBuilding.Services.Shipyards;

namespace ShipBuilding.Web.ViewModels.Shipyards
{
    public class ShipyardSelectionViewModel
    {
        public ShipyardsFilter Filter { get; set; }
        public IEnumerable<ShipyardBrowsingViewModel> FilteredData { get; private set; }
        public ShipyardBrowsingViewModel SelectedObject { get; set; } = new();

        /// <summary>Стовпець, за яким відсортовано список зараз.</summary>
        public string SortBy { get; }

        public SortingOrder Order { get; }

        /// <param name="data">Верфі, уже відфільтровані й упорядковані сервісом.</param>
        public ShipyardSelectionViewModel(ShipyardsFilter filter, ShipyardSortField sortBy, SortingOrder order,
            IEnumerable<ShipyardBrowsingViewModel> data)
        {
            Filter = filter;
            SortBy = sortBy.ToString();
            Order = order;
            FilteredData = data.ToList();
        }

        /// <summary>
        /// Напрям для посилання в заголовку стовпця: повторний клік по поточному стовпцю
        /// сортує у зворотному порядку, клік по іншому — за зростанням.
        /// </summary>
        public SortingOrder NextOrder(string column) =>
            column == SortBy && Order == SortingOrder.Ascending ? SortingOrder.Descending : SortingOrder.Ascending;
    }
}
