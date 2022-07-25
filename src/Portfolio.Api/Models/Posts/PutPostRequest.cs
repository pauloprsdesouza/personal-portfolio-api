using System.ComponentModel.DataAnnotations;
using Portfolio.Domain.Posts;

namespace Portfolio.Api.Models.Posts
{
    public class PutPostRequest
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
        public int CategoryId { get; set; }

        /// <summary>
        /// Post's front image URL
        /// </summary>
        [Required]
        public string FrontImageUrl { get; set; }

        /// <summary>
        /// Post's reading time
        /// </summary>
        [Required]
        public int ReadingTime { get; set; }

        /// <summary>
        /// Post's status
        /// </summary>
        [Required]
        public PostStatusEnum Status { get; set; }

        /// <summary>
        /// Post's views
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// Post's content
        /// </summary>
        [Required]
        public string Content { get; set; }

        public void MapTo(Post post)
        {
            post.Title = Title;
            post.Subtitle = Subtitle;
            post.CategoryId = CategoryId;
            post.Status = Status;
            post.FrontImageUrl = FrontImageUrl;
            post.ReadingTime = ReadingTime;
            post.Content = Content;
        }
    }
}
