using System.Threading.Tasks;
using Portfolio.Domain.Subscribers;

namespace Portfolio.Api.Features.Subscribers
{
    public class SubscriberSearch
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberSearch(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public bool SubscriberNotFound { get; private set; }

        public async Task<Subscriber> Find(int subscriberId)
        {
            var subscriber = await _subscriberRepository.FindById(subscriberId);

            if (subscriber == null)
            {
                SubscriberNotFound = true;
                return null;
            }

            return subscriber;
        }
    }
}
