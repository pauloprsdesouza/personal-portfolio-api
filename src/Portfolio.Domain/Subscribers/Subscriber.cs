namespace Portfolio.Domain.Subscribers
{
    public class Subscriber
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
