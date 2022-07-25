namespace Portfolio.Domain.Posts
{
    public interface IPostRepository
    {
        Task<Post> FindById(int id);

        Task<Post> Create(Post post);

        Task<Post> Update(Post post);

        Task<Post> Delete(Post post);
    }
}
