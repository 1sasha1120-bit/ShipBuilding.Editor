namespace ShipBuilding.Web.Infrastructure.Paging
{
    public static class PagingMethods
    {
        public const int DefaultPageSize = 10;

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source,
            int page, int pageSize = DefaultPageSize)
        {
            var all = source as IReadOnlyCollection<T> ?? source.ToList();
            var paging = new PagingInfo(all.Count, pageSize, page);

            var items = all
                .Skip((paging.CurrentPage - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToList();

            return new PagedList<T>(items, paging);
        }

        public static IReadOnlyList<int?> GetPageNumbers(this PagingInfo paging, int neighbours = 2)
        {
            var shown = Enumerable.Range(1, paging.TotalPages)
                .Where(p => p == 1 || p == paging.TotalPages
                         || Math.Abs(p - paging.CurrentPage) <= neighbours);

            var result = new List<int?>();
            int? previous = null;
            foreach (int p in shown)
            {
                if (previous is int q)
                {
                    if (p - q == 2) result.Add(q + 1);
                    else if (p - q > 2) result.Add(null);
                }
                result.Add(p);
                previous = p;
            }
            return result;
        }
    }
}
