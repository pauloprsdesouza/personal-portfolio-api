using System.ComponentModel.DataAnnotations;
using Portfolio.Domain.Users;

namespace Portfolio.Api.Models.Users
{
    public class UserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User ToUser()
        {
            return new User
            {
                Email = Email,
                Password = BCrypt.Net.BCrypt.HashPassword(Password)
            };
        }
    }
}
