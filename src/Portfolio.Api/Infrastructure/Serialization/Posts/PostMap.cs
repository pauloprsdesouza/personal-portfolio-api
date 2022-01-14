using Portfolio.Api.Infrastructure.Database.DataModel.Posts;
using Portfolio.Api.Models.Posts;

namespace Portfolio.Api.Infrastructure.Serialization.Posts
{
    public static class PostMap
    {
        public static PostResponse MapToResponse(this Post post)
        {
            return new PostResponse
            {
                Id = post.Id.ToString(),
                Title = post.Title,
                Subtitle = post.Subtitle,
                Status = post.Status,
                Content = post.Content,
                FrontImageUrl = post.FrontImageUrl,
                ReadingTime = post.ReadingTime,
                Views = post.Views,
                UpdatedAt = post.UpdatedAt,
                CreatedAt = post.CreatedAt
            };
        }
    }
}
