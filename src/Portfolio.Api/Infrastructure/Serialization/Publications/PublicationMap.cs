using Portfolio.Api.Infrastructure.Database.DataModel.Publications;
using Portfolio.Api.Models.Publications;

namespace Portfolio.Api.Infrastructure.Serialization.Publications
{
    public static class PublicationMap
    {
        public static PublicationResponse MapToResponse(this Publication publication)
        {
            return new PublicationResponse
            {
                Id = publication.Id.ToString(),
                Title = publication.Title,
                Abstract = publication.Abstract,
                Publisher = publication.Publisher,
                Year = publication.Year,
                Volume = publication.Volume,
                Page = publication.Page,
                Type = publication.Type,
                Qualis = publication.Qualis,
                UrlPublication = publication.UrlPublication
            };
        }
    }
}
