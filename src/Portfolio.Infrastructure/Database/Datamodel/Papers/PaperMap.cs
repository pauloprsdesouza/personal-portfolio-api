using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Papers;

namespace Portfolio.Infrastructure.Database.Datamodel.Papers
{
    public static class PaperMap
    {
        public static void Configure(this EntityTypeBuilder<Paper> builder)
        {
            builder.ToTable("paper");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Page);

            builder.Property(p => p.Qualis);

            builder.Property(p => p.Abstract);

            builder.Property(p => p.Title);

            builder.Property(p => p.Type);

            builder.Property(p => p.Volume);

            builder.Property(p => p.Year);

            builder.Property(p => p.UrlPublication);

            builder.Property(p => p.Publisher);

            builder.Property(p => p.CreatedAt);

            builder.Property(p => p.UpdatedAt);
        }
    }
}
