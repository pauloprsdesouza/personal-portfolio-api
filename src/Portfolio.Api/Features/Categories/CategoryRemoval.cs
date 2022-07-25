using System.Threading.Tasks;
using Portfolio.Domain.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class CategoryRemoval
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryRemoval(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool CategoryNotFound { get; private set; }

        public async Task<Category> Delete(int categoryId)
        {
            var categorySearch = new CategorySearch(_categoryRepository);
            var category = await categorySearch.Find(categoryId);

            if (categorySearch.CategoryNotFound)
            {
                CategoryNotFound = true;
                return null;
            }

            await _categoryRepository.Delete(category);

            return category;
        }
    }
}
