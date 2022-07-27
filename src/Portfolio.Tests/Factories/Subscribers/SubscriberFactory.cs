using System;
using Bogus;
using Portfolio.Domain.Subscribers;

namespace Portfolio.Tests.Factories.Subscribers
{
    public static class SubscriberFactory
    {
        public static Subscriber Build(this Subscriber subscriber)
        {
            var subscriberFaker = new Faker<Subscriber>()
            .RuleFor(p => p.Email, f => f.Person.Email)
            .RuleFor(p => p.CreatedAt, f => DateTimeOffset.Now);

            return subscriberFaker.Generate();
        }
    }
}
