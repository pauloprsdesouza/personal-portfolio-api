using Portfolio.Domain.Categories;

namespace Portfolio.Api.Models.Categories
{
    public static class CategoryMapResponse
    {
        public static CategoryResponse MapToResponse(this Category category)
        {
            return new CategoryResponse()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
