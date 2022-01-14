using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Posts
{
    public class PostNotFoundError : IActionResult
    {
        public PostNotFoundError() { }

        public PostNotFoundError(string postId)
        {
            PostId = postId;
        }

        /// <summary>
        /// Post's ID not found.
        /// </summary>
        /// <value></value>
        public string PostId { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this);
            jsonResult.StatusCode = StatusCodes.Status404NotFound;

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
