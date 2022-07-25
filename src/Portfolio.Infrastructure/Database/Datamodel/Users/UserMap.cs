using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Users;

namespace Portfolio.Infrastructure.Database.Datamodel.Users
{
    public static class UserMap
    {
        public static void Configure(this EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name);

            builder.Property(p => p.Email);

            builder.Property(p => p.Password);

            builder.Property(p => p.ProfileImageUrl);

            builder.HasMany(p => p.Posts)
                   .WithOne(p => p.Publisher)
                   .HasForeignKey(p => p.PublisherId);
        }
    }
}
