using System;

namespace Portfolio.Api.Models.Papers
{
    public class PaperResponse
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

        /// <summary>
        /// When paper was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// When paper was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}
