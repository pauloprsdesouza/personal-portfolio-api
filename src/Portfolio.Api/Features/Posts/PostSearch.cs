using System.Threading.Tasks;
using Portfolio.Domain.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class PostSearch
    {
        public readonly IPostRepository _postRepository;

        public PostSearch(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool PostNotFound { get; private set; }

        public async Task<Post> Find(int postId)
        {
            var post = await _postRepository.FindById(postId);

            if (post == null)
            {
                PostNotFound = true;
                return null;
            }

            return post;
        }
    }
}
