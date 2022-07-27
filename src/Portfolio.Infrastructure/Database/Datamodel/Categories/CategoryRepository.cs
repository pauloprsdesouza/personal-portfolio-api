using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Categories;

namespace Portfolio.Infrastructure.Database.Datamodel.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApiDbContext _dbContext;

        private readonly DbSet<Category> _categories;

        public CategoryRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _categories = dbContext.Set<Category>();
        }

        public async Task<Category> Create(Category category)
        {
            await _categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Delete(Category category)
        {
            _categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> FindAll()
        {
            return await _categories.ToListAsync();
        }

        public async Task<Category> FindById(int id)
        {
            return await _categories
                .Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Category> Update(Category category)
        {
            _categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
    }
}
