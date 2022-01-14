using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class PostUpdate
    {
        private readonly IDynamoDBContext _dbContext;

        public PostUpdate(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Update(Post post)
        {
            post.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveAsync(post);
        }
    }
}
