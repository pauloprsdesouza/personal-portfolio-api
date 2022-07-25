using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Subscribers;

namespace Portfolio.Infrastructure.Database.Datamodel.Subscribers
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly ApiDbContext _dbContext;

        private readonly DbSet<Subscriber> _subscribers;

        public async Task<Subscriber> Create(Subscriber subscriber)
        {
            await _subscribers.AddAsync(subscriber);
            await _dbContext.SaveChangesAsync();

            return subscriber;
        }

        public async Task<Subscriber> Delete(Subscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            await _dbContext.SaveChangesAsync();

            return subscriber;
        }

        public async Task<Subscriber> FindById(int id)
        {
            return await _subscribers.Where(p => p.Id == id).SingleOrDefaultAsync();
        }
    }
}
