using Reddit.Models;

namespace Reddit.Repositories
{
    public interface IPostsRepository
    {
        public Task<PagedList<Post>> GetPosts(int page, int pageSize, string? searchTerm, string? SortTerm, bool? isAscending = true);
    }
}
