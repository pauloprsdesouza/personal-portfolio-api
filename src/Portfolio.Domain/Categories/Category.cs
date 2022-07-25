using Portfolio.Domain.Posts;

namespace Portfolio.Domain.Categories
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public List<Post> Posts { get; set; }
    }
}
