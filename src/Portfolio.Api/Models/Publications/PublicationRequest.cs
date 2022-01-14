using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Infrastructure.Database.DataModel.Publications;

namespace Portfolio.Api.Models.Publications
{
    public class PublicationRequest
    {
        [Required, MaxLength(150)]
        public string Title { get; set; }

        [Required, MaxLength(1000)]
        public string Abstract { get; set; }

        [Required, MaxLength(150)]
        public string Publisher { get; set; }

        [Required]
        public string Year { get; set; }

        public string Volume { get; set; }

        public string Page { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Qualis { get; set; }

        [Required]
        public string UrlPublication { get; set; }

        public Publication ToPublication()
        {
            return new Publication
            {
                Title = Title,
                Abstract = Abstract,
                Publisher = Publisher,
                Year = Year,
                Volume = Volume,
                Page = Page,
                Type = Type,
                Qualis = Qualis,
                UrlPublication = UrlPublication

            };
        }
    }
}
