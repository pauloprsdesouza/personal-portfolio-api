using System;
using System.Threading.Tasks;
using Portfolio.Api.Models.Posts;
using Portfolio.Domain.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class PostUpdate
    {
        private readonly IPostRepository _postRepository;

        public PostUpdate(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool PostNotFound { get; private set; }

        public async Task<Post> Update(int postId, PutPostRequest postRequest)
        {
            var postSearch = new PostSearch(_postRepository);
            var post = await postSearch.Find(postId);

            if (postSearch.PostNotFound)
            {
                PostNotFound = true;
                return null;
            }

            postRequest.MapTo(post);

            post.UpdatedAt = DateTimeOffset.UtcNow;

            await _postRepository.Update(post);

            return post;
        }
    }
}
