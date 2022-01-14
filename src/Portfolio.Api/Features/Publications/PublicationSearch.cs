using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Publications;

namespace Portfolio.Api.Features.Publications
{
    public class PublicationSearch
    {
         public readonly IDynamoDBContext _dbContext;

        public PublicationSearch(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool PublicationNotFound { get; private set; }

        public async Task<Publication> Find(string id)
        {
            var publicationKey = new PublicationKey(id);

            var publication = await _dbContext.LoadAsync<Publication>(publicationKey.PK, publicationKey.SK);

            PublicationNotFound = publication == null;

            return publication;
        }
    }
}
