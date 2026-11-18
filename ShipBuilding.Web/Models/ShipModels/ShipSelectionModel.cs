using Common.Types;
using ShipBuilding.Entities;
using ShipBuilding.Web.Models.Ships;

namespace ShipBuilding.Web.Models.ShipModels
{
    public class ShipsSortingInfo
    {
        public SortingOrder NameOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder RegistrationNumberOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder LaunchDateOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder ShipyardNameOrder { get; set; } = SortingOrder.Ascending;
        public SortingOrder TypeOfShipNameOrder { get; set; } = SortingOrder.Ascending;
    }

    public class ShipSelectionModel
    {
        public ShipSelectionModel(ShipsFilter filter, ShipsSortingInfo sortingInfo, 
            IEnumerable<Ship> data, string sortFieldName)
        {
            Filter = filter;
            SortingInfo = sortingInfo;
            ApplyFilter(data);
            ApplySorting(sortFieldName);
        }

        public ShipsFilter Filter { get; set; } = new();
        public IEnumerable<ShipBrowsingModel> FilteredData { get; private set; } = null!;
        public ShipsSortingInfo SortingInfo { get; set; } = new();

        //public ShipBrowsingModel SelectedObject { get; set; } = new();


        private void ApplyFilter(IEnumerable<Ship> data)
        {
            IEnumerable<Ship> filteredData = data;
            if (!string.IsNullOrWhiteSpace(Filter.Name))
            {
                string nameFilter = Filter.Name.Trim().ToLower();
                filteredData = filteredData.Where(e =>
                        e.Name?.Contains(nameFilter,
                        StringComparison.OrdinalIgnoreCase) == true);
            }
            if (!string.IsNullOrWhiteSpace(Filter.RegistrationNumber))
            {
                string regFilter = Filter.RegistrationNumber.Trim().ToLower();
                filteredData = filteredData.Where(e =>
            e.RegistrationNumber?.Contains(regFilter, StringComparison.OrdinalIgnoreCase) == true);
            }
            if (Filter.LaunchDateMin.HasValue)
            {
                filteredData = filteredData.Where(e => e.LaunchDate >= Filter.LaunchDateMin.Value);
            }
            if (Filter.LaunchDateMax.HasValue)
            {
                filteredData = filteredData.Where(e => e.LaunchDate <= Filter.LaunchDateMax.Value);
            }
            if (!string.IsNullOrWhiteSpace(Filter.ShipyardName))
            {
                string shipyardNameFilter = Filter.ShipyardName.Trim().ToLower();
                filteredData = filteredData.Where(e =>
                        e.Shipyard?.Name?.Contains(shipyardNameFilter, 
                        StringComparison.OrdinalIgnoreCase) == true);
            }
            if (!string.IsNullOrWhiteSpace(Filter.TypeOfShipName))
            {
                string typeOfShipNameFilter = Filter.TypeOfShipName.Trim().ToLower();
                filteredData = filteredData.Where(e =>
                        e.TypeOfShip?.Name?.Contains(typeOfShipNameFilter,
                        StringComparison.OrdinalIgnoreCase) == true);
            }
            FilteredData = filteredData.Select(e => (ShipBrowsingModel)e).OrderBy(e => e.Name);
        }

        private void ApplySorting(string sortFieldName)
        {
            if (string.IsNullOrWhiteSpace(sortFieldName))
            {
                return;
            }
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
                case "RegistrationNumber":
                    if (SortingInfo.RegistrationNumberOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.RegistrationNumber);
                        SortingInfo.RegistrationNumberOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.RegistrationNumber);
                        SortingInfo.RegistrationNumberOrder = SortingOrder.Ascending;
                    }
                    break;
                case "LaunchDate":
                    if (SortingInfo.LaunchDateOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.LaunchDate);
                        SortingInfo.LaunchDateOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.LaunchDate);
                        SortingInfo.LaunchDateOrder = SortingOrder.Ascending;
                    }
                    break;
                case "ShipyardName":
                    if (SortingInfo.ShipyardNameOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.ShipyardName);
                        SortingInfo.ShipyardNameOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.ShipyardName);
                        SortingInfo.ShipyardNameOrder = SortingOrder.Ascending;
                    }
                    break;
                case "TypeOfShipName":
                    if (SortingInfo.TypeOfShipNameOrder == SortingOrder.Ascending)
                    {
                        FilteredData = FilteredData.OrderBy(e => e.TypeOfShipName);
                        SortingInfo.TypeOfShipNameOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        FilteredData = FilteredData.OrderByDescending(e => e.TypeOfShipName);
                        SortingInfo.TypeOfShipNameOrder = SortingOrder.Ascending;
                    }
                    break;
            }
        }
    }
}
