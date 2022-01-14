using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models.Contacts
{
    public class ContactRequest
    {
        [Required, MaxLength(150)]
        public string From { get; set; }

        [Required, MaxLength(150)]
        public string Subject { get; set; }

        [Required, MaxLength(500)]
        public string Content { get; set; }
    }
}
