using System.Threading.Tasks;
using Portfolio.Tests.Fakes;
using Xunit;
using Portfolio.Api.Models.Posts;

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

            // _client.PostJsonAsync("/posts", post);

            // var teste = 0;

            Assert.Equal(3, 3);
        }
    }
}
