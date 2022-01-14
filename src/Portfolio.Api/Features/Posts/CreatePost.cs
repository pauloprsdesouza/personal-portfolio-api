using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;
using NUlid;
using Portfolio.Api.Features.Users;

namespace Portfolio.Api.Features.Posts
{
    public class CreatePost
    {
        private readonly IDynamoDBContext _dbContext;

        public CreatePost(IDynamoDBContext dbContext){
            _dbContext = dbContext;
        }

        public async Task Register(string userId, string userEmail, Post post){
            var userSearch = new UserSearch(_dbContext);
            var user = await userSearch.Find(userEmail);

            post.Id = Ulid.NewUlid();
            post.CreatedAt = DateTimeOffset.UtcNow;
            post.PostedBy = userId;
            post.Views = "0";

            var postKey = new PostKey(post.Id);

            postKey.AssignTo(post);

            await _dbContext.SaveAsync(post);
        }
    }
}
