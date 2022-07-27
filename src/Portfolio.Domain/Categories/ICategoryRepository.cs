namespace Portfolio.Domain.Categories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> FindAll();

        Task<Category> FindById(int id);

        Task<Category> Create(Category category);

        Task<Category> Update(Category category);

        Task<Category> Delete(Category category);
    }
}
