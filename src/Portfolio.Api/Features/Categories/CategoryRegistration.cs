using System;
using System.Threading.Tasks;
using Portfolio.Domain.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class CategoryRegistration
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryRegistration(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Register(Category category)
        {
            category.CreatedAt = DateTimeOffset.UtcNow;

            await _categoryRepository.Create(category);

            return category;
        }
    }
}
