using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Posts;

namespace Portfolio.Infrastructure.Database.Datamodel.Posts
{
    public class PostRepository : IPostRepository
    {
        private ApiDbContext _dbContext;

        private DbSet<Post> _posts;

        public PostRepository(ApiDbContext dbContext) {
            _dbContext = dbContext;
            _posts = dbContext.Set<Post>();
        }

        public async Task<Post> Create(Post post)
        {
            await _posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> Delete(Post post)
        {
            _posts.Remove(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> FindById(int id)
        {
            return await _posts.Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Post> Update(Post post)
        {
            _posts.Update(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }
    }
}
