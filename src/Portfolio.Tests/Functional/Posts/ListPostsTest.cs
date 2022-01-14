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
        public async Task ShouldFind()
        {
            PostRequest post = new PostRequest();
            post.Title = "Teste";

            var response = await _client.PostJsonAsync("/posts", post);

            var responseJson = await response.Content.ReadAsStringAsync();

            var jsonOption = new JsonSerializerOptions().Default();

            var responseDoAmor = JsonSerializer.Deserialize<PostResponse>(responseJson, jsonOption);

            Assert.Equal(post.Title, responseDoAmor.Title);
        }
    }
}
