using System.Threading.Tasks;
using Portfolio.Domain.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class CategorySearch
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategorySearch(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool CategoryNotFound { get; private set; }

        public async Task<Category> Find(int categoryId)
        {
            var category = await _categoryRepository.FindById(categoryId);

            if (category == null)
            {
                CategoryNotFound = true;
                return null;
            }

            return category;
        }
    }
}
