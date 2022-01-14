namespace Portfolio.Api.Infrastructure.Database.DataModel.Subscribers
{
    public class SubscriberKey
    {
        public SubscriberKey(string subscriberId)
        {
            PK = $"Subscriber";
            SK = $"Email#{subscriberId}";
        }

        public string PK { get; }

        public string SK { get; }

        public void AssignTo(Subscriber subscriber)
        {
            subscriber.PK = PK;
            subscriber.SK = SK;
        }
    }
}
