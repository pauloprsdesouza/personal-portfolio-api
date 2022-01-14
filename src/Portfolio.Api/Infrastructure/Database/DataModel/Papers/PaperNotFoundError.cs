using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Papers
{
    public class PaperNotFoundError : IActionResult
    {
        public PaperNotFoundError() { }

        public PaperNotFoundError(string paperId)
        {
            PapaerId = paperId;
        }

        /// <summary>
        /// Post's ID not found.
        /// </summary>
        /// <value></value>
        public string PapaerId { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this);
            jsonResult.StatusCode = StatusCodes.Status404NotFound;

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
