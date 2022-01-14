using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Categories
{
    public class CategoryNotFoundError : IActionResult
    {
        public CategoryNotFoundError() { }

        public CategoryNotFoundError(string categoryId)
        {
            CategoryId = categoryId;
        }

        /// <summary>
        /// Post's ID not found.
        /// </summary>
        /// <value></value>
        public string CategoryId { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this);
            jsonResult.StatusCode = StatusCodes.Status404NotFound;

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
