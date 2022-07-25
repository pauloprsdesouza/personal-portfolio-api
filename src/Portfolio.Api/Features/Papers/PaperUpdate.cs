using System;
using System.Threading.Tasks;
using Portfolio.Api.Models.Papers;
using Portfolio.Domain.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class PaperUpdate
    {
        private readonly IPaperRepository _paperRepository;

        public PaperUpdate(IPaperRepository paperRepository)
        {
            _paperRepository = paperRepository;
        }

        public bool PaperNotFound { get; private set; }

        public async Task<Paper> Update(int paperId, PutPaperRequest paperRequest)
        {
            var paperSearch = new PaperSearch(_paperRepository);
            var paper = await paperSearch.Find(paperId);

            if (paperSearch.PaperNotFound)
            {
                PaperNotFound = true;
                return null;
            }

            paperRequest.MapTo(paper);
            paper.UpdatedAt = DateTimeOffset.UtcNow;

            await _paperRepository.Update(paper);

            return paper;
        }
    }
}
