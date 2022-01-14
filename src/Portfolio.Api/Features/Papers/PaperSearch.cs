using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class PaperSearch
    {
        public readonly IDynamoDBContext _dbContext;

        public PaperSearch(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool PaperNotFound { get; private set; }

        public async Task<Paper> Find(string id)
        {
            var paperKey = new PaperKey(id);

            var paper = await _dbContext.LoadAsync<Paper>(paperKey.PK, paperKey.SK);

            PaperNotFound = paper == null;

            return paper;
        }
    }
}
