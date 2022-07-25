using Portfolio.Domain.Papers;

namespace Portfolio.Api.Models.Papers
{
    public static class PaperMapResponse
    {
        public static PaperResponse MapToResponse(this Paper paper)
        {
            return new PaperResponse()
            {
                Title = paper.Title,
                Abstract = paper.Abstract,
                Publisher = paper.Publisher,
                Year = paper.Year,
                Volume = paper.Volume,
                Page = paper.Page,
                Type = paper.Type,
                Qualis = paper.Qualis,
                UrlPublication = paper.UrlPublication,
                CreatedAt = paper.CreatedAt,
                UpdatedAt = paper.UpdatedAt
            };
        }
    }
}
