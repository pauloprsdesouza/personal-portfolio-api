using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Models.Categories;
using Portfolio.Api.Features.Categories;
using Portfolio.Api.Models;
using Portfolio.Domain.Categories;
using System.Linq;

namespace Portfolio.Api.Controllers
{
    [Route("api/v1/categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// List all categories
        /// </summary>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> List()
        {
            var categories = await _categoryRepository.FindAll();

            return Ok(new GetCategoryResponse()
            {
                Categories = categories.Select(p => p.MapToResponse())
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
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] PostCategoryRequest categoryRequest)
        {
            var createCategory = new CategoryRegistration(_categoryRepository);
            var category = categoryRequest.ToCategory();

            await createCategory.Register(category);

            return Ok(category.MapToResponse());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryRequest"></param>
        /// <returns></returns>
        [HttpPut, Route("{categoryId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Update([FromRoute] int categoryId, [FromBody] PutCategoryRequest categoryRequest)
        {
            var categoryUpdate = new CategoryUpdate(_categoryRepository);
            var category = await categoryUpdate.Update(categoryId, categoryRequest);

            if (categoryUpdate.CategoryNotFound)
            {
                return UnprocessableEntity(new ResponseError("CATEGORY_NOT_FOUND"));
            }

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
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] int categoryId)
        {
            var categorySearch = new CategorySearch(_categoryRepository);
            var category = await categorySearch.Find(categoryId);

            if (categorySearch.CategoryNotFound)
            {
                return NotFound(new ResponseError("CATEGORY_NOT_FOUND"));
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
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] int categoryId)
        {
            var categoryRemoval = new CategoryRemoval(_categoryRepository);
            var category = await categoryRemoval.Delete(categoryId);

            if (categoryRemoval.CategoryNotFound)
            {
                return UnprocessableEntity(new ResponseError("CATEGORY_NOT_FOUND"));
            }

            return Ok(category.MapToResponse());
        }
    }
}
