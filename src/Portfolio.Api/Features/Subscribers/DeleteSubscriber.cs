using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Subscribers;

namespace Portfolio.Api.Features.Subscribers
{
    public class DeleteSubscriber
    {
        public readonly IDynamoDBContext _dbContext;

        public DeleteSubscriber(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Subscriber subscriber)
        {
            await _dbContext.DeleteAsync(subscriber);
        }
    }
}
