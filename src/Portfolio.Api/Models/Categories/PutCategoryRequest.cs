using System.ComponentModel.DataAnnotations;
using Portfolio.Domain.Categories;

namespace Portfolio.Api.Models.Categories
{
    public class PutCategoryRequest
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        public void MapTo(Category category)
        {
            category.Name = Name;
        }
    }
}
