using System;
using System.Threading.Tasks;
using Portfolio.Domain.Posts;

namespace Portfolio.Api.Features.Posts
{
    public class PostRegistration
    {
        private readonly IPostRepository _postRepository;

        public PostRegistration(IPostRepository postRepository){
            _postRepository = postRepository;
        }

        public async Task<Post> Register(Post post){
            post.CreatedAt = DateTimeOffset.UtcNow;

            await _postRepository.Create(post);

            return post;
        }
    }
}
