using NUlid;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Posts
{
    public class PostKey
    {
        public PostKey(Ulid postId)
        {
            PK = $"Post";
            SK = $"Id#{postId}";
        }

        public string PK { get; }

        public string SK { get; }

        public void AssignTo(Post post)
        {
            post.PK = PK;
            post.SK = SK;
        }
    }
}
