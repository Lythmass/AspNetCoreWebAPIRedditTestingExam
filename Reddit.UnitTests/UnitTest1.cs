using Microsoft.EntityFrameworkCore;
using Reddit;
using Reddit.Models;
using Reddit.Repositories;

namespace Reddit.UnitTests;

public class UnitTest1
{

    private IPostsRepository GetPostsRepostory()
    {

        var dbName = Guid.NewGuid().ToString();     // give unqie name to the database, so that different tests don't interfere with each other
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var dbContext = new ApplicationDbContext(options);

        dbContext.Posts.Add(new Post { Title = "Title 1", Content = "Content 1", Upvotes = 5, Downvotes = 1 });
        dbContext.Posts.Add(new Post { Title = "Title 2", Content = "Content 1", Upvotes = 12, Downvotes = 1 });
        dbContext.Posts.Add(new Post { Title = "Title 3", Content = "Content 1", Upvotes = 3, Downvotes = 1 });
        dbContext.Posts.Add(new Post { Title = "Title 4", Content = "Content 1", Upvotes = 221, Downvotes = 1 }); // 221 / 222 = 0.9954954954954955
        dbContext.Posts.Add(new Post { Title = "Title 5", Content = "Content 1", Upvotes = 5, Downvotes = 2123 }); // 5 / 2123 = 0.002356344
        dbContext.SaveChanges();

        return new PostsRepository(dbContext);
    }

    [Fact]
    public async Task GetPosts_ReturnsCorrectPagination()
    {
        var postsRepository = GetPostsRepostory();
        var posts = await postsRepository.GetPosts(1, 2, null, null, null);

        Assert.Equal(2, posts.Items.Count);
        Assert.Equal(5, posts.TotalCount);
        Assert.True(posts.HasNextPage);
        Assert.False(posts.HasPreviousPage);
    }


    [Fact]
    public async Task GetPosts_ReturnsCorrect()
    {
        var postsRepository = GetPostsRepostory();
        var posts = await postsRepository.GetPosts(1, 2, null, "popular", false);

        Assert.Equal(2, posts.Items.Count);
        Assert.Equal(5, posts.TotalCount);
        Assert.True(posts.HasNextPage);
        Assert.False(posts.HasPreviousPage);

        //  Positivity -> Assert.Equal("Title 4", posts.Items.First().Title); 
        Assert.Equal("Title 5", posts.Items.First().Title);
    }

    [Fact]
    public async Task GetPosts_InvalidPage_ThrowsArgumentException()
    {
        var repository = GetPostsRepostory();

        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.GetPosts(page: 0, pageSize: 10, searchTerm: null, SortTerm: null));
        Assert.Equal("page", exception.ParamName);
    }

    [Fact]
    public async Task GetPosts_InvalidPageSize_ThrowsArgumentOutOfRangeException()
    {
        var repository = GetPostsRepostory();

        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.GetPosts(page: 1, pageSize: 0, searchTerm: null, SortTerm: null));
        Assert.Equal("pageSize", exception.ParamName);
    }
}