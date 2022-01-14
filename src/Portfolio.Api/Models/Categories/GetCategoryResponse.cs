using System.Collections.Generic;

namespace Portfolio.Api.Models.Categories
{
    public class GetCategoryResponse
    {
        /// <summary>
        /// Messa unique identifier
        /// </summary>
        public IEnumerable<CategoryResponse> Categories { get; set; }
    }
}
