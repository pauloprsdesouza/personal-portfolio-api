using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Features.Posts;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;

namespace Portfolio.Api.Models.Posts
{
    public class PostRequest
    {
        /// <summary>
        /// Post's title
        /// </summary>
        [Required, MaxLength(150)]
        public string Title { get; set; }

        /// <summary>
        /// Post's subtitle
        /// </summary>
        [Required, MaxLength(200)]
        public string Subtitle { get; set; }

        /// <summary>
        /// Post's category id
        /// </summary>
        [Required]
        public string CategoryId { get; set; }

        /// <summary>
        /// Post's front image URL
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// Post's reading time
        /// </summary>
        [Required]
        public string FrontImageUrl { get; set; }

        /// <summary>
        /// Post's reading time
        /// </summary>
        [Required]
        public string ReadingTime { get; set; }

        /// <summary>
        /// Post's content that will be read by a user
        /// </summary>
        /// <value></value>
        [Required]
        public string Content { get; set; }

        public Post ToPost()
        {
            return new Post
            {
                Title = Title,
                Subtitle = Subtitle,
                CategoryId = CategoryId,
                Status = Status,
                FrontImageUrl = FrontImageUrl,
                ReadingTime = ReadingTime,
                Content = Content
            };
        }
    }
}
