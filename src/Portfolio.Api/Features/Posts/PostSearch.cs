using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;
using NUlid;

namespace Portfolio.Api.Features.Posts
{
    public class PostSearch
    {
        public readonly IDynamoDBContext _dbContext;

        public PostSearch(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool PostNotFound { get; private set; }

        public async Task<Post> Find(Ulid postId)
        {
            var postKey = new PostKey(postId);

            var post =  await _dbContext.LoadAsync<Post>(postKey.PK, postKey.SK);

            PostNotFound = post == null;

            return post;
        }
    }
}
