using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class PaperUpdate
    {
        private readonly IDynamoDBContext _dbContext;

        public PaperUpdate(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Update(Paper paper)
        {
            paper.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveAsync(paper);
        }
    }
}
