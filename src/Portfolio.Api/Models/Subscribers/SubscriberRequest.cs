using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Infrastructure.Database.DataModel.Subscribers;

namespace Portfolio.Api.Models.Subscribers
{
    public class SubscriberRequest
    {
        [Required, MaxLength(150)]
        public string Email { get; set; }

        public Subscriber ToSubscriber()
        {
            return new Subscriber
            {
                Email = Email
            };
        }
    }
}
