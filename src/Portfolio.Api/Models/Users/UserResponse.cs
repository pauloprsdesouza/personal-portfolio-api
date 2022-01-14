using System;

namespace Portfolio.Api.Models.Users
{
    public class UserResponse
    {
         public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ProfileImageUrl { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
