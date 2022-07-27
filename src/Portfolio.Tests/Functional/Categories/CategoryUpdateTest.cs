using System.Net;
using System.Threading.Tasks;
using Portfolio.Api.Models.Categories;
using Portfolio.Domain.Categories;
using Portfolio.Tests.Factories.Categories;
using Portfolio.Tests.Fakes;
using Xunit;

namespace Portfolio.Tests.Functional.Categories
{
    public class CategoryUpdateTest
    {
        private readonly FakeApiServer _server;
        private readonly FakeApiClient _client;

        public CategoryUpdateTest()
        {
            _server = new FakeApiServer();
            _client = new FakeApiClient(_server);
        }

        [Fact]
        public async Task ShouldRegistrate()
        {
            var category = new Category().Build();

            await _server.DataBase.Categories.AddAsync(category);
            await _server.DataBase.SaveChangesAsync();

            var categoryJson = category.ToJson();
            categoryJson.Name = "New Category";

            var response = await _client.PutJsonAsync($"api/v1/categories/{category.Id}", categoryJson);
            var categoryResponse = await _client.ReadAsJsonAsync<CategoryResponse>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(categoryJson.Name, categoryResponse.Name);
        }
    }
}
