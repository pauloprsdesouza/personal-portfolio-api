using System.ComponentModel.DataAnnotations;
using NUlid;

namespace Portfolio.Api.Models.Posts
{
    public class GetPostsQuery
    {
        /// <summary>
        /// Posts less than Post ID before
        /// </summary>
        public Ulid? Before { get; set; }

        /// <summary>
        /// Post's title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post's category ID
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Post's status, where it can be technology, productivity, etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Post's length
        /// </summary>
        [MaxLength(100)]
        public int? Length { get; set; }
    }
}
