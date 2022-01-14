using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Publications;

namespace Portfolio.Api.Features.Publications
{
    public class PublicationUpdate
    {
        private readonly IDynamoDBContext _dbContext;

        public PublicationUpdate(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Update(Publication publication)
        {
            publication.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveAsync(publication);
        }
    }
}
