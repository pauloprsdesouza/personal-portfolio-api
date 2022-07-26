using Portfolio.Domain.Subscribers;

namespace Portfolio.Api.Models.Subscribers
{
    public static class SubscriberMapResponse
    {
        public static SubscriberResponse MapToResponse(this Subscriber subscriber)
        {
            return new SubscriberResponse()
            {
                Id = subscriber.Id,
                Email = subscriber.Email,
                CreatedAt = subscriber.CreatedAt

            };
        }
    }
}
