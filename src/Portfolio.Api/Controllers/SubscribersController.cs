using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Infrastructure.Database.DataModel.Subscribers;
using Portfolio.Api.Models.Subscribers;
using System.Linq;
using Portfolio.Api.Infrastructure.Serialization.Subscribers;
using Portfolio.Api.Features.Subscribers;
using NUlid;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Api.Features.Messages;

namespace Portfolio.Api.Controllers
{
    [Route("Subscribers")]
    public class SubscribersController : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        public SubscribersController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
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
            var query = new SubscriberQuery();

            var subscribers = await _dbContext
                .FromQueryAsync<Subscriber>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            return Ok(new GetSubscriberResponse
            {
                Subscribers = subscribers.Select(subscriber => subscriber.MapToResponse())
            });
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
            var createSubscriber = new CreateSubscriber(_dbContext);
            var subscriber = subscriberRequest.ToSubscriber();

            await createSubscriber.Register(subscriber);

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
        [ProducesResponseType(typeof(SubscriberNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] Ulid subscriberId)
        {
            var subscriberSearch = new SubscriberSearch(_dbContext);
            var subscriber = await subscriberSearch.Find(subscriberId.ToString());

            if (subscriberSearch.SubscriberNotFound)
            {
                return NotFound(new SubscriberNotFoundError(subscriberId.ToString()));
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
        [ProducesResponseType(typeof(SubscriberNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Ulid subscriberId)
        {
            var subscriberSearch = new SubscriberSearch(_dbContext);
            var subscriber = await subscriberSearch.Find(subscriberId.ToString());

            if (subscriberSearch.SubscriberNotFound)
            {
                return NotFound(new SubscriberNotFoundError(subscriberId.ToString()));
            }

            var subscriberDelete = new DeleteSubscriber(_dbContext);
            await subscriberDelete.Delete(subscriber);

            return Ok(subscriber.MapToResponse());
        }
    }
}
