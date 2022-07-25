using System.Threading.Tasks;
using Portfolio.Domain.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class PostViewsUpdate
    {
        private readonly IPostRepository _postRepository;

        public PostViewsUpdate(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool PostNotFound { get; private set; }

        public async Task<Post> Update(int postId)
        {
            var postSearch = new PostSearch(_postRepository);
            var post = await postSearch.Find(postId);

            if (post == null)
            {
                PostNotFound = true;
                return null;
            }

            post.Views++;

            await _postRepository.Update(post);

            return post;
        }

    }
}
