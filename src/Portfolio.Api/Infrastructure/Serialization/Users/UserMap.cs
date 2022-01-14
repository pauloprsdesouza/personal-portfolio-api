using Portfolio.Api.Infrastructure.Database.DataModel.Users;
using Portfolio.Api.Models.Users;

namespace Portfolio.Api.Infrastructure.Serialization.Users
{
    public static class UserMap
    {
        public static UserResponse MapToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                UpdatedAt = user.UpdatedAt,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
