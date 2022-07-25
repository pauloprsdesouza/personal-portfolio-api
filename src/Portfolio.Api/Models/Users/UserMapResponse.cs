using Portfolio.Domain.Users;

namespace Portfolio.Api.Models.Users
{
    public static class UserMapResponse
    {
        public static UserResponse MapToResponse(this User user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
