using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Domain.Papers;

namespace Portfolio.Api.Features.Papers
{
    public class PaperRegistration
    {
        private readonly IPaperRepository _paperRepository;

        public PaperRegistration(IPaperRepository paperRepository)
        {
            _paperRepository = paperRepository;
        }

        public async Task<Paper> Register(Paper paper)
        {
            paper.CreatedAt = DateTimeOffset.UtcNow;

            await _paperRepository.Create(paper);

            return paper;
        }
    }
}
