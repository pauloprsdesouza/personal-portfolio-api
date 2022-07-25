using System.Threading.Tasks;
using Portfolio.Domain.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class PostArchivement
    {
        public readonly IPostRepository _postRepository;

        public PostArchivement(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool PostNotFound {get; private set;}

        public async Task<Post> Delete(int postId)
        {
            var postSearch = new PostSearch(_postRepository);
            var post = await postSearch.Find(postId);

            if(postSearch.PostNotFound) {
                PostNotFound = true;
                return null;
            }

            post.Status = PostStatusEnum.Archived;

            await _postRepository.Update(post);

            return post;
        }
    }
}
