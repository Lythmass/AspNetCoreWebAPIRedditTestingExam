using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reddit;
using Reddit.Models;
using Reddit.Repositories;

namespace RedditTests
{
    
    public class UnitTest1
    {
        private ApplicationDbContext CreateContext()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName).Options;
            var context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(
                new Post { Title = "Title 1", Content = "Content 1", Upvotes = 1, Downvotes = 1 },
                new Post { Title = "Title 2", Content = "Content 2", Upvotes = 1, Downvotes = 1 },
                new Post { Title = "Title 3", Content = "Content 3", Upvotes = 1, Downvotes = 1 },
                new Post { Title = "Title 4", Content = "Content 4", Upvotes = 1, Downvotes = 1 },
                new Post { Title = "Title 5", Content = "Content 5", Upvotes = 1, Downvotes = 1 },
                new Post { Title = "Title 6", Content = "Content 6", Upvotes = 1, Downvotes = 1 },
                new Post { Title = "Title 7", Content = "Content 7", Upvotes = 1, Downvotes = 1 }
            );

            context.SaveChanges();
            return context;
        }
        [Fact]
        public async Task GetPosts_Pagination_ReturnsCorrectSizeAndPage()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var pagedPosts = await repository.GetPosts(1, 3, null, null, null);

            Assert.Equal("Title 1", pagedPosts.Items.First().Title);
            Assert.Equal("Title 3", pagedPosts.Items.Last().Title);
            Assert.Equal(3, pagedPosts.PageSize);
        }

        [Fact]

        public async Task GetPosts_Pagination_ReturnsNextPageTrue()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var pagedPosts = await repository.GetPosts(1, 3, null, null, null);

            Assert.True(pagedPosts.HasNextPage);
            Assert.False(pagedPosts.HasPreviousPage);
        }

        [Fact]
        public async Task GetPosts_Pagination_ReturnsPreviousPageTrue()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var pagedPosts = await repository.GetPosts(3, 3, null, null, null);

            Assert.True(pagedPosts.HasPreviousPage);
            Assert.False(pagedPosts.HasNextPage);
        }

        [Fact]
        public async Task GetPosts_Pagination_ReturnsEmpty()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var emptyList = await repository.GetPosts(1, 3, "Hello", null, null);

            Assert.Equal(0, emptyList.Items.Count);
            Assert.False(emptyList.HasPreviousPage);
            Assert.False(emptyList.HasNextPage);
        }

        [Fact]
        public async Task GetPosts_Pagination_ReturnsPageSizeGreaterThanTotal()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var pagedPosts = await repository.GetPosts(1, 10, null, null, null);

            Assert.True(pagedPosts.PageSize > pagedPosts.TotalCount);
            Assert.False(pagedPosts.HasPreviousPage);
            Assert.False(pagedPosts.HasNextPage);
            Assert.Equal("Title 1", pagedPosts.Items.First().Title);
            Assert.Equal("Title 7", pagedPosts.Items.Last().Title);
            Assert.Equal(7, pagedPosts.Items.Count);
        }

        [Fact]
        public async Task GetPosts_Pagination_ReturnsTotalGreaterThanPageSize()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var pagedPosts = await repository.GetPosts(2, 1, null, null, null);

            Assert.True(pagedPosts.PageSize < pagedPosts.TotalCount);
            Assert.True(pagedPosts.HasPreviousPage);
            Assert.True(pagedPosts.HasNextPage);
            Assert.Equal("Title 2", pagedPosts.Items.First().Title);
            Assert.Equal(1, pagedPosts.Items.Count);
        }

        [Fact]
        public async Task GetPosts_Pagination_PageThrowsErrorOnZeroValues()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.GetPosts(0, 1, null, null, null));

            Assert.Equal("page", exception.ParamName);
        }

        [Fact]
        public async Task GetPosts_Pagination_PageSizeThrowsErrorOnZeroValues()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.GetPosts(1, 0, null, null, null));

            Assert.Equal("pageSize", exception.ParamName);
        }

        [Fact]
        public async Task GetPosts_Pagination_ReturnsEmptyOnPageGreaterThanTotalCount()
        {
            var context = CreateContext();
            var repository = new PostsRepository(context);

            var pagedPosts = await repository.GetPosts(10, 2, null, null, null);

            Assert.Equal(0, pagedPosts.Items.Count);
        }
    }
}