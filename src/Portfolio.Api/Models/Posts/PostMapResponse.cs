using Portfolio.Domain.Posts;

namespace Portfolio.Api.Models.Posts
{
    public static class PostMapResponse
    {
        public static PostResponse MapToResponse(this Post post)
        {
            return new PostResponse() {
                Id = post.Id,
                Title = post.Title,
                Category = post.Category.Name,
                Subtitle = post.Subtitle,
                Views = post.Views,
                Content = post.Content,
                FrontImageUrl = post.FrontImageUrl,
                ReadingTime = post.ReadingTime,
                Status = post.Status,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };
        }
    }
}
