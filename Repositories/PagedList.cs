using Microsoft.EntityFrameworkCore;

namespace Reddit.Repositories
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int page, int pageSize, int count, bool hasNextPage, bool hasPreviousPage)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = count;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> items, int page, int pageSize) {
            var pagedItems = await items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalCount = await items.CountAsync();
            var hasNextPage = (page * pageSize) < totalCount;
            var hasPreviousPage = page > 1;

            return new PagedList<T>(pagedItems, page, pageSize, totalCount, hasNextPage, hasPreviousPage);
        }
    }
}
