using Portfolio.Domain.Papers;

namespace Portfolio.Api.Models.Papers
{
    public class PutPaperRequest
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

        public void MapTo(Paper paper)
        {
            paper.Title = Title;
            paper.Abstract = Abstract;
            paper.Publisher = Publisher;
            paper.Year = Year;
            paper.Volume = Volume;
            paper.Page = Page;
            paper.Type = Type;
            paper.Qualis = Qualis;
            paper.UrlPublication = UrlPublication;
        }
    }
}
