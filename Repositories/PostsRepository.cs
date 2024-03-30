using Microsoft.EntityFrameworkCore;
using Reddit.Models;

namespace Reddit.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplcationDBContext _context;

        public PostsRepository(ApplcationDBContext applcationDBContext)
        {
            _context = applcationDBContext;
        }
        public async Task<PagedList<Post>> GetPosts(int page, int pageSize, string? searchTerm)
        {
            var posts =  _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                posts = posts.Where(post => 
                     post.Title.Contains(searchTerm) || post.Content.Contains(searchTerm));
            }

            return await PagedList<Post>.CreateAsync(posts, page, pageSize);
        }
    }
}
