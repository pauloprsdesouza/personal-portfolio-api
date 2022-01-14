using System;

namespace Portfolio.Api.Models.Papers
{
    public class PaperResponse
    {
        /// <summary>
        /// Paper's ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Paper's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Paper's submission deadline.
        /// </summary>
        public string SubmissionDeadline { get; set; }

        /// <summary>
        /// Paper's type, where it can be J = Journal and C = Conference.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Paper's place, where is a place that will happen the event if it was a conference type.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Paper's qualis. Here, it will be used the Brazilian's Qualis.
        /// </summary>
        public string Qualis { get; set; }

        /// <summary>
        /// Paper's website URL of the event.
        /// </summary>
        public string WebsiteUrl { get; set; }

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
