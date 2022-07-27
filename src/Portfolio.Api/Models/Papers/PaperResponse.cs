using System;
using Portfolio.Domain.Papers;

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

        public PaperType Type { get; set; }

        public QualisEnum Qualis { get; set; }

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
