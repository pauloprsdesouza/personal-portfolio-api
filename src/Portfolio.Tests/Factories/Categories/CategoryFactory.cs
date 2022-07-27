using System;
using Bogus;
using Portfolio.Domain.Categories;

namespace Portfolio.Tests.Factories.Categories
{
    public static class CategoryFactory
    {
        public static Category Build(this Category category)
        {
            var categoryFaker = new Faker<Category>()
            .RuleFor(p => p.Name, f => f.Lorem.Word())
            .RuleFor(p => p.CreatedAt, f => DateTime.Now);

            return categoryFaker.Generate();
        }
    }
}
