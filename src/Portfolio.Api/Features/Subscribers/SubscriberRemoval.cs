using System.Threading.Tasks;
using Portfolio.Domain.Subscribers;

namespace Portfolio.Api.Features.Subscribers
{
    public class SubscriberRemoval
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberRemoval(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public bool SubscriberNotFound { get; private set; }

        public async Task<Subscriber> Delete(int subscriberId)
        {
            var subscriberSearch = new SubscriberSearch(_subscriberRepository);
            var subscriber = await subscriberSearch.Find(subscriberId);

            if (subscriberSearch.SubscriberNotFound)
            {
                SubscriberNotFound = true;
                return null;
            }

            await _subscriberRepository.Delete(subscriber);

            return subscriber;
        }
    }
}
