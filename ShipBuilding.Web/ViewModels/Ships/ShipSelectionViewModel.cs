using Common.Types;
using ShipBuilding.Services.Ships;

namespace ShipBuilding.Web.ViewModels.Ships
{
    public class ShipSelectionViewModel
    {
        /// <param name="data">Судна, уже відфільтровані й упорядковані сервісом.</param>
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

        /// <summary>Стовпець, за яким відсортовано список зараз.</summary>
        public string SortBy { get; }

        public SortingOrder Order { get; }

        /// <summary>
        /// Напрям для посилання в заголовку стовпця: повторний клік по поточному стовпцю
        /// сортує у зворотному порядку, клік по іншому — за зростанням.
        /// </summary>
        public SortingOrder NextOrder(string column) =>
            column == SortBy && Order == SortingOrder.Ascending ? SortingOrder.Descending : SortingOrder.Ascending;
    }
}
