using Microsoft.EntityFrameworkCore;
using Reddit.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext applcationDBContext)
        {
            _context = applcationDBContext;
        }
        public async Task<PagedList<Post>> GetPosts(int page, int pageSize, string? searchTerm, string? SortTerm, bool? isAscending = true)
        {
            var posts = _context.Posts.AsQueryable();

            if (isAscending == false) {
                posts = posts.OrderByDescending(GetSortExpression(SortTerm));
             //   posts = posts.OrderDescending(GetSortExpression(searchTerm));

            }
            else
            {
                posts = posts.OrderBy(GetSortExpression(SortTerm));
            }


            if (!string.IsNullOrEmpty(searchTerm))
            {
                posts = posts.Where(post => 
                     post.Title.Contains(searchTerm) || post.Content.Contains(searchTerm));
            }

            return await PagedList<Post>.CreateAsync(posts, page, pageSize);
        }

        public Expression<Func<Post, object>> GetSortExpression(string? sortTerm) {
        sortTerm = sortTerm?.ToLower();
            return sortTerm switch
            {
            "positivity" => post => (post.Upvotes) / (post.Upvotes + post.Downvotes),
            "popular" => post => post.Upvotes + post.Downvotes,
             _ => post => post.Id
         };
        }
    }
}
