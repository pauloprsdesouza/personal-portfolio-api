using Portfolio.Domain.Posts;

namespace Portfolio.Domain.Users
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ProfileImageUrl { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public List<Post> Posts { get; set; }
    }
}
