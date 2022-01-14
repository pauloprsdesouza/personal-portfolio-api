using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class CategoryUpdate
    {
        private readonly IDynamoDBContext _dbContext;

        public CategoryUpdate(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Update(Category category)
        {
            category.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveAsync(category);
        }
    }
}
