using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Publications
{
    public class PublicationNotFoundError : IActionResult
    {
        public PublicationNotFoundError() { }

        public PublicationNotFoundError(string publicationId)
        {
            PublicationId = publicationId;
        }

        /// <summary>
        /// Post's ID not found.
        /// </summary>
        /// <value></value>
        public string PublicationId { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this);
            jsonResult.StatusCode = StatusCodes.Status404NotFound;

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
