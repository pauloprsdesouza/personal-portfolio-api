using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Infrastructure.Database.DataModel.Users;

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
