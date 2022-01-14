using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Subscribers;

namespace Portfolio.Api.Features.Subscribers
{
    public class CreateSubscriber
    {
        private readonly IDynamoDBContext _dbContext;

        public CreateSubscriber(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Register(Subscriber subscriber)
        {
            var subscriberSearch = new SubscriberSearch(_dbContext);

            subscriber.Id = Ulid.NewUlid();
            subscriber.CreatedAt = DateTimeOffset.UtcNow;

            var subscriberKey = new SubscriberKey(subscriber.Id.ToString());

            subscriberKey.AssignTo(subscriber);

            await _dbContext.SaveAsync(subscriber);
        }
    }
}
