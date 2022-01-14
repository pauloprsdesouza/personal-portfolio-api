using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Subscribers;

namespace Portfolio.Api.Features.Subscribers
{
    public class SubscriberSearch
    {
         public readonly IDynamoDBContext _dbContext;

        public SubscriberSearch(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool SubscriberNotFound { get; private set; }

        public async Task<Subscriber> Find(string subscriberId)
        {
            var subscriberKey = new SubscriberKey(subscriberId);

            var subscriber =  await _dbContext.LoadAsync<Subscriber>(subscriberKey.PK, subscriberKey.SK);

            SubscriberNotFound = subscriber == null;

            return subscriber;
        }
    }
}
