using Portfolio.Api.Infrastructure.Database.DataModel.Subscribers;
using Portfolio.Api.Models.Subscribers;

namespace Portfolio.Api.Infrastructure.Serialization.Subscribers
{
    public static class SubscriberMap
    {

        public static SubscriberResponse MapToResponse(this Subscriber subscriber)
        {
            return new SubscriberResponse
            {
                Id = subscriber.Id.ToString(),
                Email = subscriber.Email,
                CreatedAt = subscriber.CreatedAt
            };
        }
    }
}
