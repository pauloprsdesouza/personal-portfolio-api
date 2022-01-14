using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class DeletePost
    {
        public readonly IDynamoDBContext _dbContext;

        public DeletePost(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Post post)
        {
            post.Status = "A"; //Archived;
            await _dbContext.SaveAsync(post);
        }
    }
}
