namespace Portfolio.Domain.Subscribers
{
    public interface ISubscriberRepository
    {
        Task<Subscriber> FindById(int id);

        Task<Subscriber> Create(Subscriber subscriber);

        Task<Subscriber> Delete(Subscriber subscriber);
    }
}
