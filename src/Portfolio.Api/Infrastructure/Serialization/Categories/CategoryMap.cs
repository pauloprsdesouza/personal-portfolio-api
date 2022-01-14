using Portfolio.Api.Infrastructure.Database.DataModel.Categories;
using Portfolio.Api.Models.Categories;

namespace Portfolio.Api.Infrastructure.Serialization.Categories
{
    public static class CategoryMap
    {
        public static CategoryResponse MapToResponse(this Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id.ToString(),
                Name = category.Name
            };
        }
    }
}
