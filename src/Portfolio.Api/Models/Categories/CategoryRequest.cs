using System.ComponentModel.DataAnnotations;
using Portfolio.Domain.Categories;

namespace Portfolio.Api.Models.Categories
{
    public class CategoryRequest
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        public Category ToCategory()
        {
            return new Category
            {
                Name = Name
            };
        }
    }
}
