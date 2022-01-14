using System.Threading.Tasks;
using Portfolio.Tests.Fakes;
using Xunit;
using Portfolio.Api.Models.Posts;
using System.Text.Json;
using Portfolio.Api.Configuration;

namespace Portfolio.Tests.Functional.Posts
{
    public class ListPostsTest
    {
        private readonly FakeApiServer _server;
        private readonly FakeApiClient _client;

        public ListPostsTest()
        {
            _server = new FakeApiServer();
            _client = new FakeApiClient(_server);
        }

        [Fact]
        public async Task ShouldCreatePost()
        {
            PostRequest post = new PostRequest();
            post.Title = "Teste";
                post.Title = "Title";
                post.Subtitle = "Subtitle";
                post.CategoryId = "01FPSZ1HQYS0G5N44GA43KZSKZ";
                post.Status = "P";
                post.FrontImageUrl = "https://";
                post.ReadingTime = "12";
                post.Content = "Content";

            var response = await _client.PostJsonAsync("/posts", post);

            var responseJson = await response.Content.ReadAsStringAsync();

            var jsonOption = new JsonSerializerOptions().Default();

            var postResponse = JsonSerializer.Deserialize<PostResponse>(responseJson, jsonOption);

            Assert.Equal(post.Title, postResponse.Title);
            Assert.Equal(post.Subtitle, postResponse.Subtitle);
            Assert.Equal(post.Status, postResponse.Status);
            Assert.Equal(post.FrontImageUrl, postResponse.FrontImageUrl);
            Assert.Equal(post.ReadingTime, postResponse.ReadingTime);
            Assert.Equal(post.Content, postResponse.Content);
        }
    }
}
