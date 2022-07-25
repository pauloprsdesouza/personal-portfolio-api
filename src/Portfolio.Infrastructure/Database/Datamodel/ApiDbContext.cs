using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Categories;
using Portfolio.Domain.Papers;
using Portfolio.Domain.Posts;
using Portfolio.Domain.Subscribers;
using Portfolio.Domain.Users;
using Portfolio.Infrastructure.Database.Datamodel.Papers;
using Portfolio.Infrastructure.Database.Datamodel.Users;
using Portfolio.Infrastructure.Database.Datamodel.Subscribers;
using Portfolio.Infrastructure.Database.Datamodel.Categories;
using Portfolio.Infrastructure.Database.Datamodel.Posts;

namespace Portfolio.Infrastructure.Database.Datamodel
{
    public class ApiDbContext : DbContext
    {
        public const string Schema = "portfolio";

        public ApiDbContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Paper> Papers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<Paper>().Configure();
            modelBuilder.Entity<User>().Configure();
            modelBuilder.Entity<Subscriber>().Configure();
            modelBuilder.Entity<Category>().Configure();
            modelBuilder.Entity<Post>().Configure();
        }
    }
}
