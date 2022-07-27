namespace Portfolio.Domain.Papers
{
    public class Paper
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public string Publisher { get; set; }

        public int Year { get; set; }

        public int Volume { get; set; }

        public int Page { get; set; }

        public PaperType Type { get; set; }

        public QualisEnum Qualis { get; set; }

        public string UrlPublication { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
