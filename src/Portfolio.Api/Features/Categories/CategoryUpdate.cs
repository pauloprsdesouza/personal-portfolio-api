using System;
using System.Threading.Tasks;
using Portfolio.Api.Models.Categories;
using Portfolio.Domain.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class CategoryUpdate
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryUpdate(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool CategoryNotFound { get; private set; }

        public async Task<Category> Update(int categoryId, PutCategoryRequest categoryRequest)
        {
            var categorySearch = new CategorySearch(_categoryRepository);
            var category = await categorySearch.Find(categoryId);

            if (categorySearch.CategoryNotFound)
            {
                CategoryNotFound = true;
                return null;
            }

            categoryRequest.MapTo(category);

            category.UpdatedAt = DateTimeOffset.UtcNow;

            await _categoryRepository.Update(category);

            return category;
        }
    }
}
