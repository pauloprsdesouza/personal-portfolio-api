using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Publications;

namespace Portfolio.Api.Features.Publications
{
    public class DeletePublication
    {
        public readonly IDynamoDBContext _dbContext;

        public DeletePublication(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Publication publication)
        {
            await _dbContext.DeleteAsync(publication);
        }
    }
}
