using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Categories;
using Portfolio.Api.Models.Categories;
using System.Linq;
using Portfolio.Api.Infrastructure.Serialization.Categories;
using Portfolio.Api.Features.Categories;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Api.Controllers
{
    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        public CategoriesController(IDynamoDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        /// <summary>
        /// Get categories
        /// </summary>
        /// <remarks>
        /// Categories that represent a post's topic.
        /// </remarks>
        [HttpGet, AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> List([FromQuery] GetCategoriesQuery queryString)
        {
            var query = new CategoryQuery();

            var categories = await _dbContext
                .FromQueryAsync<Category>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            return Ok(new GetCategoryResponse
            {
                Categories = categories.Select(category => category.MapToResponse())
            });
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <remarks>
        /// This category contains many posts.
        /// </remarks>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] CategoryRequest categoryRequest)
        {
            var createCategory = new CreateCategory(_dbContext);
            var category = categoryRequest.ToCategory();

            await createCategory.Register(category);

            return Ok(category.MapToResponse());
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <remarks>
        /// Update a category assigned to a post.
        /// </remarks>
        /// <param name="categoryId" example="01FME0F949HAVJ91A9100N16ZS">Category's ID</param>
        /// <param name="putCategoryRequest">Category's content</param>
        [HttpPut, Route("{categoryId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] Ulid categoryId, [FromBody] PutCategoryRequest putCategoryRequest)
        {
            var categorySearch = new CategorySearch(_dbContext);
            var category = await categorySearch.Find(categoryId.ToString());

            if (categorySearch.CategoryNotFound)
            {
                return NotFound(new CategoryNotFoundError(categoryId.ToString()));
            }

            putCategoryRequest.MapTo(category);

            var categoryUpdate = new CategoryUpdate(_dbContext);
            await categoryUpdate.Update(category);

            return Ok(category.MapToResponse());
        }

        /// <summary>
        /// Find a category
        /// </summary>
        /// <remarks>
        /// Find a category already registered.
        /// </remarks>
        /// <param name="categoryId" example="01FME0F949HAVJ91A9100N16ZS">Category's ID</param>
        [HttpGet, Route("{categoryId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] Ulid categoryId)
        {
            var categorySearch = new CategorySearch(_dbContext);
            var category = await categorySearch.Find(categoryId.ToString());

            if (categorySearch.CategoryNotFound)
            {
                return NotFound(new CategoryNotFoundError(categoryId.ToString()));
            }

            return Ok(category.MapToResponse());
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <remarks>
        /// Delete a category already registered.
        /// </remarks>
        /// <param name="categoryId" example="01FME0F949HAVJ91A9100N16ZS">Category's ID</param>
        [HttpDelete, Route("{categoryId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Ulid categoryId)
        {
            var categorySearch = new CategorySearch(_dbContext);
            var category = await categorySearch.Find(categoryId.ToString());

            if (categorySearch.CategoryNotFound)
            {
                return NotFound(new CategoryNotFoundError(categoryId.ToString()));
            }

            var categoryDelete = new DeleteCategory(_dbContext);
            await categoryDelete.Delete(category);

            return Ok(category.MapToResponse());
        }
    }
}
