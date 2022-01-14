using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class CreatePaper
    {
        private readonly IDynamoDBContext _dbContext;

        public CreatePaper(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Register(Paper paper)
        {
            paper.Id = Ulid.NewUlid();
            paper.CreatedAt = DateTimeOffset.UtcNow;

            var postKey = new PaperKey(paper.Id.ToString());

            postKey.AssignTo(paper);

            await _dbContext.SaveAsync(paper);
        }
    }
}
