using System;
using System.ComponentModel.DataAnnotations;
using NUlid;

namespace Portfolio.Api.Models.Papers
{
    public class GetPapersQuery
    {
         /// <summary>
        /// Paper's type, where it can be J = Journal and C = Conference.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Paper's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Paper's submission deadline.
        /// </summary>
        public string SubmissionDeadline { get; set; }

        /// <summary>
        /// Paper's qualis. Here, it will be used the Brazilian's Qualis.
        /// </summary>
        public string Qualis { get; set; }

        /// <summary>
        /// Paper's length.
        /// </summary>
        [MaxLength(100)]
        public int? Length { get; set; }
    }
}
