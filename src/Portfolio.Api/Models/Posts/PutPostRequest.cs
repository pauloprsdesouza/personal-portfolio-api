using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;

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
        public string CategoryId { get; set; }

        /// <summary>
        /// Post's front image URL
        /// </summary>
        [Required]
        public string FrontImageUrl { get; set; }

        /// <summary>
        /// Post's reading time
        /// </summary>
        [Required]
        public string ReadingTime { get; set; }

        /// <summary>
        /// Post's status
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// Post's views
        /// </summary>
        public bool Views { get; set; }

        /// <summary>
        /// Post's content
        /// </summary>
        [Required]
        public string Content { get; set; }

        public void MapTo(Post post)
        {
            if (Views)
            {
                post.Views = post.Views + 1;
            }

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
