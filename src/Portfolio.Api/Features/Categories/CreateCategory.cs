using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class CreateCategory
    {
        private readonly IDynamoDBContext _dbContext;

        public CreateCategory(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Register(Category category)
        {
            category.Id = Ulid.NewUlid();
            category.CreatedAt = DateTimeOffset.UtcNow;

            var postKey = new CategoryKey(category.Name);

            postKey.AssignTo(category);

            await _dbContext.SaveAsync(category);
        }
    }
}
