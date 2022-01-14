using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Users
{
    public class UserNotFoundError : IActionResult
    {
        public UserNotFoundError() { }

        public UserNotFoundError(string userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Post's ID not found.
        /// </summary>
        /// <value></value>
        public string UserId { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this);
            jsonResult.StatusCode = StatusCodes.Status404NotFound;

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
