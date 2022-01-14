using System.ComponentModel.DataAnnotations;
using NUlid;

namespace Portfolio.Api.Models.Categories
{
    public class GetCategoriesQuery
    {
        /// <summary>
        /// Messages less than Message ID before
        /// </summary>
        public Ulid? Before {get;set;}

        /// <summary>
        /// Message's length
        /// </summary>
        [MaxLength(100)]
        public int? Length {get;set;}
    }
}
