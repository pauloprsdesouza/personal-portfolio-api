using Portfolio.Domain.Papers;

namespace Portfolio.Api.Models.Papers
{
    public class PaperRequest
    {
        public string Title { get; set; }

        public string Abstract { get; set; }

        public string Publisher { get; set; }

        public int Year { get; set; }

        public int Volume { get; set; }

        public int Page { get; set; }

        public string Type { get; set; }

        public string Qualis { get; set; }

        public string UrlPublication { get; set; }

        public Paper ToPaper()
        {
            return new Paper
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
