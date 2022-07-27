using System;
using Bogus;
using Portfolio.Domain.Posts;

namespace Portfolio.Tests.Factories.Posts
{
    public static class PostFactory
    {
        public static Post Build(this Post post)
        {
            var postFaker = new Faker<Post>()
            .RuleFor(p => p.Content, f => f.Lorem.Text())
            .RuleFor(p => p.FrontImageUrl, f => f.Image.PlaceImgUrl())
            .RuleFor(p => p.Views, f => f.Random.Int())
            .RuleFor(p => p.Title, f => f.Lorem.Sentence())
            .RuleFor(p => p.CreatedAt, f => DateTimeOffset.Now)
            .RuleFor(p => p.Subtitle, f => f.Lorem.Sentence())
            .RuleFor(p => p.ReadingTime, f => f.Random.Int())
            .RuleFor(p => p.Status, f => f.PickRandom<PostStatusEnum>());

            return postFaker.Generate();
        }
    }
}

