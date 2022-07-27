using System.Net;
using System.Threading.Tasks;
using Portfolio.Api.Models.Categories;
using Portfolio.Domain.Categories;
using Portfolio.Tests.Factories.Categories;
using Portfolio.Tests.Fakes;
using Xunit;

namespace Portfolio.Tests.Functional.Categories
{
    public class CategoryRegistrationTest
    {
        private readonly FakeApiServer _server;
        private readonly FakeApiClient _client;

        public CategoryRegistrationTest()
        {
            _server = new FakeApiServer();
            _client = new FakeApiClient(_server);
        }

        [Fact]
        public async Task ShouldRegistrate()
        {
            var categoryJson = new Category().Build().ToJson();

            var response = await _client.PostJsonAsync("api/v1/categories", categoryJson);
            var categoryResponse = await _client.ReadAsJsonAsync<CategoryResponse>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(categoryJson.Name, categoryResponse.Name);
        }
    }
}
