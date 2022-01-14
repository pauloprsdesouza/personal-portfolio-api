using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Categories;
using System.Linq;

namespace Portfolio.Api.Features.Categories
{
    public class CategorySearch
    {
        public readonly IDynamoDBContext _dbContext;

        public CategorySearch(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CategoryNotFound { get; private set; }

        public async Task<Category> Find(string categoryId)
        {
            var categoryKey = new CategoryKey(String.Empty);

            var category = await _dbContext.LoadAsync<Category>(categoryKey.PK, categoryKey.SK);

            CategoryNotFound = category == null;

            return category;
        }
    }
}
