using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Posts;

namespace Portfolio.Infrastructure.Database.Datamodel.Posts
{
    public static class PostMap
    {
        public static void Configure(this EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("post");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.PublisherId).ValueGeneratedNever();

            builder.Property(p => p.CategoryId).ValueGeneratedNever();

            builder.Property(p => p.Title);

            builder.Property(p => p.Subtitle);

            builder.Property(p => p.Content);

            builder.Property(p => p.FrontImageUrl);

            builder.Property(p => p.ReadingTime);

            builder.Property(p => p.Status);

            builder.Property(p => p.Views);

            builder.Property(p => p.CreatedAt);

            builder.Property(p => p.UpdatedAt);
        }
    }
}
