using Microsoft.EntityFrameworkCore;
using Reddit.Models;

namespace Reddit
{
    public class ApplcationDBContext: DbContext
    {
        public ApplcationDBContext(DbContextOptions<ApplcationDBContext> dbContextOptions): base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDatabase.db");
            optionsBuilder.LogTo(System.Console.WriteLine,LogLevel.Information);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
