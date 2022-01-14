using System;
using Portfolio.Api.Infrastructure.Database.DataModel.Users;
using Portfolio.Api.Models.Categories;
using Portfolio.Api.Models.Users;

namespace Portfolio.Api.Models.Posts
{
    public class PostResponse
    {
        /// <summary>
        /// Post's ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Post's title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post's subtitle
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Post's status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Post's front image URL
        /// </summary>
        public string FrontImageUrl { get; set; }

        /// <summary>
        /// Post's category
        /// </summary>
        public CategoryResponse Category { get; set; }

        /// <summary>
        /// Post's content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Post's reading time
        /// </summary>
        public string ReadingTime { get; set; }

        /// <summary>
        /// Post's views
        /// </summary>
        public string Views { get; set; }

        /// <summary>
        /// Post's user that updated a post
        /// </summary>
        public UserResponse PostedBy { get; set; }

        /// <summary>
        /// When this post was updated
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// When this post was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}
