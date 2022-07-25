using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Categories;

namespace Portfolio.Infrastructure.Database.Datamodel.Categories
{
    public static class CategoryMap
    {
        public static void Configure(this EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name);

            builder.HasMany(p => p.Posts)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId);
        }
    }
}
