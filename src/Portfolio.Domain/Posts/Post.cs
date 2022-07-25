using Portfolio.Domain.Categories;
using Portfolio.Domain.Users;

namespace Portfolio.Domain.Posts
{
    public class Post
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int PublisherId { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public PostStatusEnum Status { get; set; }

        public string FrontImageUrl { get; set; }

        public string Content { get; set; }

        public int ReadingTime { get; set; }

        public int Views { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Category Category { get; set; }

        public User Publisher { get; set; }
    }
}
