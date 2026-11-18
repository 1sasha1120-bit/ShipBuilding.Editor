using Common.Types;
using ShipBuilding.Entities;

namespace ShipBuilding.Web.Models.ShipyardModels
{
    public class ShipyardsSortingInfo
    {
        public SortingOrder NameOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder CountryNameOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder YearOfFoundedOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder NumberOfDocksOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder CapacityPerYearOrder { get; set; } = SortingOrder.Ascending;
    }

    public class ShipyardSelectionModel
    {
        public ShipyardsFilter Filter { get; set; } = new();
        public ShipyardsSortingInfo SortingInfo { get; set; } = new();
        public IEnumerable<ShipyardBrowsingModel> FilteredData { get; private set; } = [];
        public ShipyardBrowsingModel SelectedObject { get; set; } = new();

        public ShipyardSelectionModel(
            ShipyardsFilter filter,
            ShipyardsSortingInfo sortingInfo,
            IEnumerable<Shipyard> data,
            string? sortFieldName)
        {
            Filter = filter;
            SortingInfo = sortingInfo;
            ApplyFilter(data);
            ApplySorting(sortFieldName);
        }

        private void ApplyFilter(IEnumerable<Shipyard> data)
        {
            var result = data;
            if (!string.IsNullOrWhiteSpace(Filter.Name))
                result = result.Where(e => e.Name?.Contains(Filter.Name.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            if (!string.IsNullOrWhiteSpace(Filter.CountryName))
                result = result.Where(e => e.CountryName?.Contains(Filter.CountryName.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            if (Filter.YearOfFoundedFrom.HasValue)
                result = result.Where(e => e.YearOfFounded >= Filter.YearOfFoundedFrom.Value);
            if (Filter.YearOfFoundedTo.HasValue)
                result = result.Where(e => e.YearOfFounded <= Filter.YearOfFoundedTo.Value);

            FilteredData = result.Select(e => new ShipyardBrowsingModel
            {
                Id = e.Id,
                Name = e.Name,
                CountryName = e.CountryName,
                YearOfFounded = e.YearOfFounded,
                NumberOfDocks = e.NumberOfDocks,
                CapacityPerYear = e.CapacityPerYear
            }).OrderBy(e => e.Name);
        }

        private void ApplySorting(string? sortFieldName)
        {
            if (string.IsNullOrWhiteSpace(sortFieldName)) return;
            switch (sortFieldName)
            {
                case "Name":
                    if (SortingInfo.NameOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.Name);
                        SortingInfo.NameOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.Name);
                        SortingInfo.NameOrder = SortingOrder.Ascending;
                    }
                    break;
                case "CountryName":
                    if (SortingInfo.CountryNameOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.CountryName);
                        SortingInfo.CountryNameOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.CountryName);
                        SortingInfo.CountryNameOrder = SortingOrder.Ascending;
                    }
                    break;
                case "YearOfFounded":
                    if (SortingInfo.YearOfFoundedOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.YearOfFounded);
                        SortingInfo.YearOfFoundedOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.YearOfFounded);
                        SortingInfo.YearOfFoundedOrder = SortingOrder.Ascending;
                    }
                    break;
                case "NumberOfDocks":
                    if (SortingInfo.NumberOfDocksOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.NumberOfDocks);
                        SortingInfo.NumberOfDocksOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.NumberOfDocks);
                        SortingInfo.NumberOfDocksOrder = SortingOrder.Ascending;
                    }
                    break;
                case "CapacityPerYear":
                    if (SortingInfo.CapacityPerYearOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.CapacityPerYear);
                        SortingInfo.CapacityPerYearOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.CapacityPerYear);
                        SortingInfo.CapacityPerYearOrder = SortingOrder.Ascending;
                    }
                    break;
            }
        }
    }
}
