using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class DeletePaper
    {
        public readonly IDynamoDBContext _dbContext;

        public DeletePaper(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Paper paper)
        {
            await _dbContext.DeleteAsync(paper);
        }
    }
}
