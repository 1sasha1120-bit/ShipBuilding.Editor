using Common.Types;
using ShipBuilding.Entities;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.ShipProjectModels
{
    public class ShipProjectSelectionModel
    {
        public class ShipProjectsSortingInfo
        {
            public SortingOrder NameOrder { get; set; } = SortingOrder.Ascending;
            public SortingOrder ProjectCodeOrder { get; set; } = SortingOrder.Ascending;
            public SortingOrder ShipyardOrder { get; set; } = SortingOrder.Ascending;
            public SortingOrder BudgetOrder { get; set; } = SortingOrder.Ascending;
        }

        public ShipProjectsFilter Filter { get; set; } = new();
        public ShipProjectsSortingInfo SortingInfo { get; set; } = new();
        public IEnumerable<ShipProjectBrowsingModel> FilteredData { get; private set; } = [];
        public ShipProjectBrowsingModel SelectedObject { get; set; } = new();

        public ShipProjectSelectionModel(
            ShipProjectsFilter filter,
            ShipProjectsSortingInfo sortingInfo,
            IEnumerable<ShipProject> data,
            string? sortFieldName)
        {
            Filter = filter;
            SortingInfo = sortingInfo;
            ApplyFilter(data);
            ApplySorting(sortFieldName);
        }

        private void ApplyFilter(IEnumerable<ShipProject> data)
        {
            var result = data;
            if (!string.IsNullOrWhiteSpace(Filter.Name))
                result = result.Where(e => e.Name?.Contains(Filter.Name.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            if (!string.IsNullOrWhiteSpace(Filter.ProjectCode))
                result = result.Where(e => e.ProjectCode?.Contains(Filter.ProjectCode.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            if (!string.IsNullOrWhiteSpace(Filter.ShipyardName))
                result = result.Where(e => e.Shipyard?.Name?.Contains(Filter.ShipyardName.Trim(), StringComparison.OrdinalIgnoreCase) == true);

            FilteredData = result.Select(e => new ShipProjectBrowsingModel
            {
                Id = e.Id,
                Name = e.Name,
                ProjectCode = e.ProjectCode,
                ShipyardName = e.Shipyard?.Name,
                Budget = e.Budget,
                Description = e.Description
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
                case "ProjectCode":
                    if (SortingInfo.ProjectCodeOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.ProjectCode);
                        SortingInfo.ProjectCodeOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.ProjectCode);
                        SortingInfo.ProjectCodeOrder = SortingOrder.Ascending;
                    }
                    break;
                case "ShipyardName":
                    if (SortingInfo.ShipyardOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.ShipyardName);
                        SortingInfo.ShipyardOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.ShipyardName);
                        SortingInfo.ShipyardOrder = SortingOrder.Ascending;
                    }
                    break;
                case "Budget":
                    if (SortingInfo.BudgetOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.Budget);
                        SortingInfo.BudgetOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.Budget);
                        SortingInfo.BudgetOrder = SortingOrder.Ascending;
                    }
                    break;
            }
        }
    }
}
