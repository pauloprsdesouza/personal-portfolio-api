using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Categories;

namespace Portfolio.Api.Features.Categories
{
    public class DeleteCategory
    {
        public readonly IDynamoDBContext _dbContext;

        public DeleteCategory(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Category category)
        {
            await _dbContext.DeleteAsync(category);
        }
    }
}
