using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;

namespace Portfolio.Api.Models.Papers
{
    public class PutPaperRequest
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string SubmissionDeadline { get; set; }

        [Required]
        public string Type { get; set; }

        [Required, MaxLength(150)]
        public string Place { get; set; }

        [Required]
        public string Qualis { get; set; }

        [Required]
        public string WebsiteUrl { get; set; }

        public void MapTo(Paper paper)
        {
            paper.Title = Title;
            paper.SubmissionDeadline = SubmissionDeadline;
            paper.Type = Type;
            paper.Place = Place;
            paper.Qualis = Qualis;
            paper.WebsiteUrl = WebsiteUrl;
        }
    }
}
