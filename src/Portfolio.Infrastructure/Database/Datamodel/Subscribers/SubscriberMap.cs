using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Subscribers;

namespace Portfolio.Infrastructure.Database.Datamodel.Subscribers
{
    public static class SubscriberMap
    {
        public static void Configure(this EntityTypeBuilder<Subscriber> builder)
        {
            builder.ToTable("subscriber");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Email);
        }
    }
}
