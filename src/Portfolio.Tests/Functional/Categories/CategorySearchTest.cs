using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Portfolio.Api.Models.Categories;
using Portfolio.Domain.Categories;
using Portfolio.Tests.Factories.Categories;
using Portfolio.Tests.Fakes;
using Xunit;

namespace Portfolio.Tests.Functional.Categories
{
    public class CategorySearchTest
    {
        private readonly FakeApiServer _server;
        private readonly FakeApiClient _client;

        public CategorySearchTest()
        {
            _server = new FakeApiServer();
            _client = new FakeApiClient(_server);
        }

        [Fact]
        public async Task ShouldFind()
        {
            var category = new Category().Build();

            await _server.DataBase.Categories.AddAsync(category);
            await _server.DataBase.SaveChangesAsync();

            var response = await _client.GetAsync($"api/v1/categories/{category.Id}");
            var categoryResponse = await _client.ReadAsJsonAsync<CategoryResponse>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(category.Name, categoryResponse.Name);
        }

        [Fact]
        public async Task ShouldRespond404NotFound()
        {
            var response = await _client.GetAsync($"api/v1/categories/12354");
            var categoryResponse = await _client.ReadAsJsonAsync<JsonElement>(response);
            var error = categoryResponse.GetProperty("errors").EnumerateArray().First();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.True(error.GetString() == "CATEGORY_NOT_FOUND");
        }
    }
}
