using Portfolio.Api.Infrastructure.Database.DataModel.Papers;
using Portfolio.Api.Models.Papers;

namespace Portfolio.Api.Infrastructure.Serialization.Papers
{
    public static class PaperMap
    {
        public static PaperResponse MapToResponse(this Paper paper)
        {
            return new PaperResponse
            {
                Id = paper.Id.ToString(),
                Title = paper.Title,
                SubmissionDeadline = paper.SubmissionDeadline,
                Place = paper.Place,
                Type = paper.Type,
                Qualis = paper.Qualis,
                WebsiteUrl = paper.WebsiteUrl,
                UpdatedAt = paper.UpdatedAt,
                CreatedAt = paper.CreatedAt
            };
        }
    }
}
