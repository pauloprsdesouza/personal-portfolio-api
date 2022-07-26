using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Models.Subscribers;
using Portfolio.Api.Features.Subscribers;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Api.Features.Messages;
using Portfolio.Domain.Subscribers;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers
{
    [Route("api/v1/subscribers")]
    public class SubscribersController : Controller
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscribersController(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        /// <summary>
        /// Get subscribers
        /// </summary>
        /// <remarks>
        /// List all subscribers registered.
        /// </remarks>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetSubscriberResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> List([FromQuery] GetSubscribersQuery queryString)
        {
            return Ok();
        }

        /// <summary>
        /// Create a subscriber
        /// </summary>
        /// <remarks>
        /// Create a new subscriber to a newsletter.
        /// </remarks>
        [HttpPost, AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SubscriberResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] SubscriberRequest subscriberRequest)
        {
            var subscriberRegistration = new SubscriberRegistration(_subscriberRepository);
            var subscriber = subscriberRequest.ToSubscriber();

            await subscriberRegistration.Register(subscriber);

            var message = new SendMessage();
            message.From = subscriberRequest.Email;
            message.Subject = "Newsletter";
            message.Content = $"Thanks for subscribing! If you want to cancel your subscription, click here https://paulosouza.me/blog/cancel/subscribing/{subscriber.Id}";
            message.Source = "newsletter@paulosouza.me";

            await message.Send();

            return Ok(subscriber.MapToResponse());
        }

        /// <summary>
        /// Find a subscriber
        /// </summary>
        /// <remarks>
        /// Find a subscriber already registered.
        /// </remarks>
        /// <param name="subscriberId" example="01FME0F949HAVJ91A9100N16ZS">Subscriber's ID</param>
        [HttpGet, Route("{subscriberId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SubscriberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] int subscriberId)
        {
            var subscriberSearch = new SubscriberSearch(_subscriberRepository);
            var subscriber = await subscriberSearch.Find(subscriberId);

            if (subscriberSearch.SubscriberNotFound)
            {
                return NotFound(new ResponseError("SUBSCRIBER_NOT_FOUND"));
            }

            return Ok(subscriber.MapToResponse());
        }

        /// <summary>
        /// Delete a subscriber
        /// </summary>
        /// <remarks>
        /// Delete a subscriber already registered.
        /// </remarks>
        /// <param name="subscriberId" example="01FME0F949HAVJ91A9100N16ZS">Subscriber's ID</param>
        [HttpDelete, Route("{subscriberId}"), AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SubscriberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] int subscriberId)
        {
            var subscriberRemoval = new SubscriberRemoval(_subscriberRepository);
            var subscriber = await subscriberRemoval.Delete(subscriberId);

            if (subscriberRemoval.SubscriberNotFound)
            {
                return UnprocessableEntity(new ResponseError("SUBSCRIBER_NOT_FOUND"));
            }

            return Ok(subscriber.MapToResponse());
        }
    }
}
