using System.Threading.Tasks;
using Portfolio.Domain.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class PaperRemoval
    {
        private readonly IPaperRepository _paperRepository;

        public PaperRemoval(IPaperRepository paperRepository)
        {
            _paperRepository = paperRepository;
        }

        public bool PaperNotFound { get; private set; }

        public async Task<Paper> Delete(int paperId)
        {
            var paperSearch = new PaperSearch(_paperRepository);
            var paper = await paperSearch.Find(paperId);

            if (paper == null)
            {
                PaperNotFound = true;
                return null;
            }

            await _paperRepository.Delete(paper);

            return paper;
        }
    }
}
