using Portfolio.Api.Models.Categories;
using Portfolio.Domain.Categories;

namespace Portfolio.Tests.Factories.Categories
{
    public static class PostCategoryRequestFactory
    {
        public static PostCategoryRequest ToJson(this Category category)
        {
            return new PostCategoryRequest()
            {
                Name = category.Name
            };
        }
    }
}
