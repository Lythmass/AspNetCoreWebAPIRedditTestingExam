using Microsoft.EntityFrameworkCore;
using Reddit;
using Reddit.Models;
using Reddit.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class PostsRepositoryTests
{
    private ApplicationDbContext CreateContext()
    {
        var dbName = Guid.NewGuid().ToString();     // give unqie name to the database, so that different tests don't interfere with each other
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Posts.AddRange(
            new Post { Id = 1, Title = "First Post", Content = "First post content", Upvotes = 10, Downvotes = 2 }, // 10 / 12 = 0.8333333333333333
            new Post { Id = 2, Title = "Second Post", Content = "Second post content", Upvotes = 5, Downvotes = 1 },// 5 /6
            new Post { Id = 3, Title = "Third Post", Content = "Third post content", Upvotes = 20, Downvotes = 5 } // 20 / 25 = 0.8
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetPosts_ReturnsPagedPosts()
    {
        using var context = CreateContext();
        var repository = new PostsRepository(context);
        var pagedPosts = await repository.GetPosts(page: 1, pageSize: 2, searchTerm: null, SortTerm: null);

        Assert.Equal(2, pagedPosts.Items.Count);
        Assert.Equal(3, pagedPosts.TotalCount);
    }

    [Fact]
    public async Task GetPosts_SearchTermFiltersPosts()
    {
        using var context = CreateContext();
        var repository = new PostsRepository(context);
        var pagedPosts = await repository.GetPosts(page: 1, pageSize: 10, searchTerm: "Second", SortTerm: null);

        Assert.Single(pagedPosts.Items);
        Assert.Equal("Second Post", pagedPosts.Items.First().Title);
    }

    [Fact]
    public async Task GetPosts_SortTermSortsPosts()
    {
        using var context = CreateContext();
        var repository = new PostsRepository(context);
        var pagedPosts = await repository.GetPosts(page: 1, pageSize: 10, searchTerm: null, SortTerm: "popular", isAscending: false);

        Assert.Equal(3, pagedPosts.Items.Count);
        Assert.Equal("Third Post", pagedPosts.Items.First().Title);
    }

    [Fact]
    public async Task GetPosts_PositivitySortsPosts()
    {
        using var context = CreateContext();
        var repository = new PostsRepository(context);
        var pagedPosts = await repository.GetPosts(page: 1, pageSize: 10, searchTerm: null, SortTerm: "positivity", isAscending: false);

        Assert.Equal(3, pagedPosts.Items.Count);
        Assert.Equal("First Post", pagedPosts.Items.First().Title);
    }
    [Fact]
    public async Task GetPosts_InvalidPage_ThrowsArgumentOutOfRangeException()
    {
        using var context = CreateContext();
        var repository = new PostsRepository(context);

        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            repository.GetPosts(page: 0, pageSize: 10, searchTerm: null, SortTerm: null));

        Assert.Equal("page", exception.ParamName);
    }

    [Fact]
    public async Task GetPosts_InvalidPageSize_ThrowsArgumentOutOfRangeException()
    {
        using var context = CreateContext();
        var repository = new PostsRepository(context);

        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            repository.GetPosts(page: 1, pageSize: 0, searchTerm: null, SortTerm: null));

        Assert.Equal("pageSize", exception.ParamName);
    }
}
