using Common.Types;
using ShipBuilding.Services.ShipProjects;

namespace ShipBuilding.Web.ViewModels.ShipProjects
{
    public class ShipProjectSelectionViewModel
    {
        public ShipProjectsFilter Filter { get; set; }
        public IEnumerable<ShipProjectBrowsingViewModel> FilteredData { get; private set; }
        public ShipProjectBrowsingViewModel SelectedObject { get; set; } = new();

        /// <summary>Стовпець, за яким відсортовано список зараз.</summary>
        public string SortBy { get; }

        public SortingOrder Order { get; }

        /// <param name="data">Проєкти, уже відфільтровані й упорядковані сервісом.</param>
        public ShipProjectSelectionViewModel(ShipProjectsFilter filter, ShipProjectSortField sortBy, SortingOrder order,
            IEnumerable<ShipProjectBrowsingViewModel> data)
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
