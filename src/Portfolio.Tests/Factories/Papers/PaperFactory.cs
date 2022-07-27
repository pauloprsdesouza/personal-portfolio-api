using System;
using Bogus;
using Portfolio.Domain.Papers;

namespace Portfolio.Tests.Factories.Papers
{
    public static class PaperFactory
    {
        public static Paper Build(this Paper paper)
        {
            var paperFaker = new Faker<Paper>()
            .RuleFor(p => p.Title, f => f.Lorem.Sentence())
            .RuleFor(p => p.Abstract, f => f.Lorem.Text())
            .RuleFor(p => p.Page, f => f.Random.Int())
            .RuleFor(p => p.Qualis, f => f.PickRandom<QualisEnum>())
            .RuleFor(p => p.Type, f => f.PickRandom<PaperType>())
            .RuleFor(p => p.Volume, f => f.Random.Int())
            .RuleFor(p => p.Year, f => f.Date.Recent().Year)
            .RuleFor(p => p.UrlPublication, f => f.Internet.Url())
            .RuleFor(p => p.CreatedAt, f => DateTimeOffset.UtcNow)
            .RuleFor(p => p.Publisher, f => f.Person.FullName);

            return paperFaker.Generate();
        }
    }
}

