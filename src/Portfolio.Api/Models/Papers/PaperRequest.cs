using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;

namespace Portfolio.Api.Models.Papers
{
    public class PaperRequest
    {
        /// <summary>
        /// Paper's title.
        /// </summary>
        [Required, MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Paper's submission deadline.
        /// </summary>
        [Required]
        public string SubmissionDeadline { get; set; }

        /// <summary>
        /// Paper's type, where it can be J = Journal and C = Conference.
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Paper's place, where is a place that will happen the event if it was a conference type.
        /// </summary>
        [Required, MaxLength(150)]
        public string Place { get; set; }

        /// <summary>
        /// Paper's qualis. Here, it will be used the Brazilian's Qualis.
        /// </summary>
        [Required]
        public string Qualis { get; set; }

        /// <summary>
        /// Paper's website URL of the event.
        /// </summary>
        [Required, MaxLength(400)]
        public string WebsiteUrl { get; set; }

        public Paper ToPaper()
        {
            return new Paper
            {
                Title = Title,
                SubmissionDeadline = SubmissionDeadline,
                Type = Type,
                Place = Place,
                Qualis = Qualis,
                WebsiteUrl = WebsiteUrl
            };
        }
    }
}
