using System;
using System.Threading.Tasks;
using Portfolio.Domain.Subscribers;

namespace Portfolio.Api.Features.Subscribers
{
    public class SubscriberRegistration
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberRegistration(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public async Task<Subscriber> Register(Subscriber subscriber)
        {
            subscriber.CreatedAt = DateTimeOffset.UtcNow;

            await _subscriberRepository.Create(subscriber);

            return subscriber;
        }
    }
}
