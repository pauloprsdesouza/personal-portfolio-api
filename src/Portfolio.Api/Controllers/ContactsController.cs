using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Features.Messages;
using Portfolio.Api.Models.Contacts;

namespace Portfolio.Api.Controllers
{
    [Route("Contacts")]
    public class ContactsController : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        public ContactsController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <remarks>
        /// Send a message to user's admin
        /// </remarks>
        /// <param name="contactRequest">Contact's content</param>
        [HttpPost, AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> Send([FromBody] ContactRequest contactRequest)
        {
            var message = new SendMessage();
            message.From = contactRequest.From;
            message.Subject = $"{contactRequest.Subject} [{contactRequest.From}]" ;
            message.Content = contactRequest.Content;
            message.Source = "contact@paulosouza.me";

            await message.Send();

            return Json(message.MessageSent);
        }
    }
}
