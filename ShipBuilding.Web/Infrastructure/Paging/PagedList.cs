namespace ShipBuilding.Web.Infrastructure.Paging
{
    public class PagedList<T>
    {
        public PagedList(IReadOnlyList<T> items, PagingInfo paging)
        {
            Items = items;
            Paging = paging;
        }

        public IReadOnlyList<T> Items { get; }
        public PagingInfo Paging { get; }
    }
}
