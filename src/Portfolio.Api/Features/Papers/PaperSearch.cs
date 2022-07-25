using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Domain.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class PaperSearch
    {
         private readonly IPaperRepository _paperRepository;

        public PaperSearch(IPaperRepository paperRepository)
        {
            _paperRepository = paperRepository;
        }

        public bool PaperNotFound { get; private set; }

        public async Task<Paper> Find(int paperId)
        {
            var paper = await _paperRepository.FindById(paperId);

            if(paper == null) {
                PaperNotFound = true;
                return null;
            }

            return paper;
        }
    }
}
