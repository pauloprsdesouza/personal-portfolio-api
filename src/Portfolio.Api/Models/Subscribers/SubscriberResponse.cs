using System;

namespace Portfolio.Api.Models.Subscribers
{
    public class SubscriberResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
