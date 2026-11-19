namespace ShipBuilding.Web.Infrastructure.Paging
{
    public class PagingInfo
    {
        public PagingInfo(int totalItems, int pageSize, int requestedPage)
        {
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, null);

            TotalItems = Math.Max(0, totalItems);
            PageSize = pageSize;
            TotalPages = Math.Max(1, (int)Math.Ceiling((double)TotalItems / PageSize));
            // Номер сторінки з адресного рядка може бути будь-яким — притискаємо до меж
            CurrentPage = Math.Clamp(requestedPage, 1, TotalPages);
        }

        public int TotalItems { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int CurrentPage { get; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int FirstItemNumber => TotalItems == 0 ? 0 : (CurrentPage - 1) * PageSize + 1;

        public int LastItemNumber => Math.Min(CurrentPage * PageSize, TotalItems);
    }
}
