using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Publications;

namespace Portfolio.Api.Features.Publications
{
    public class CreatePublication
    {
        private readonly IDynamoDBContext _dbContext;

        public CreatePublication(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Register(Publication publication)
        {
            publication.Id = Ulid.NewUlid();
            publication.CreatedAt = DateTimeOffset.UtcNow;

            var postKey = new PublicationKey(publication.Id.ToString());

            postKey.AssignTo(publication);

            await _dbContext.SaveAsync(publication);
        }
    }
}
