using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Portfolio.Api.Models.Categories;
using Portfolio.Domain.Categories;
using Portfolio.Tests.Factories.Categories;
using Portfolio.Tests.Fakes;
using Xunit;

namespace Portfolio.Tests.Functional.Categories
{
    public class CategoryListTest
    {
        private readonly FakeApiServer _server;
        private readonly FakeApiClient _client;

        public CategoryListTest()
        {
            _server = new FakeApiServer();
            _client = new FakeApiClient(_server);
        }

        [Fact]
        public async Task ShouldList()
        {
            var category = new Category().Build();

            await _server.DataBase.Categories.AddAsync(category);
            await _server.DataBase.SaveChangesAsync();

            var response = await _client.GetAsync("api/v1/categories");
            var categoryResponse = await _client.ReadAsJsonAsync<GetCategoryResponse>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, categoryResponse.Categories.Count());
            Assert.True(categoryResponse.Categories.Any(p => p.Name == category.Name));
        }
    }
}
