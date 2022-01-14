using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Infrastructure.Database.DataModel.Publications;
using Portfolio.Api.Models.Publications;
using System.Linq;
using Portfolio.Api.Infrastructure.Serialization.Publications;
using Portfolio.Api.Features.Publications;
using NUlid;

namespace Portfolio.Api.Controllers
{
    [Route("Publications")]
    public class PublicationsController : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        public PublicationsController(IDynamoDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        /// <summary>
        /// Get publications
        /// </summary>
        /// <remarks>
        /// Publication's list already registered.
        /// </remarks>
        [HttpGet, AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetPublicationResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> List([FromQuery] GetPublicationsQuery queryString)
        {
            var query = new PublicationQuery();

            var projects = await _dbContext
                .FromQueryAsync<Publication>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            return Ok(new GetPublicationResponse
            {
                Publications = projects.Select(project => project.MapToResponse())
            });
        }

        /// <summary>
        /// Create a publication
        /// </summary>
        /// <remarks>
        /// Create a new publication.
        /// </remarks>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PublicationResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] PublicationRequest publicationRequest)
        {
            var createPublication = new CreatePublication(_dbContext);
            var publication = publicationRequest.ToPublication();

            await createPublication.Register(publication);

            return Ok(publication.MapToResponse());
        }

        /// <summary>
        /// Update a publication
        /// </summary>
        /// <remarks>
        /// Update a publication already registered.
        /// </remarks>
        /// <param name="publicationId" example="01FME0F949HAVJ91A9100N16ZS">Publication's ID</param>
        /// <param name="putPublicationRequest">Publication's content</param>
        [HttpPut, Route("{publicationId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PublicationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicationNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] Ulid publicationId, [FromBody] PutPublicationRequest putPublicationRequest)
        {
            var publicationSearch = new PublicationSearch(_dbContext);
            var publication = await publicationSearch.Find(publicationId.ToString());

            if (publicationSearch.PublicationNotFound)
            {
                return NotFound(new PublicationNotFoundError(publicationId.ToString()));
            }

            putPublicationRequest.MapTo(publication);

            var publicationUpdate = new PublicationUpdate(_dbContext);
            await publicationUpdate.Update(publication);

            return Ok(publication.MapToResponse());
        }

        /// <summary>
        /// Find a publication
        /// </summary>
        /// <remarks>
        /// Find a publication already registered.
        /// </remarks>
        /// <param name="publicationId" example="01FME0F949HAVJ91A9100N16ZS">Publication's ID</param>
        [HttpGet, Route("{publicationId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PublicationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicationNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] Ulid publicationId)
        {
            var publicationSearch = new PublicationSearch(_dbContext);
            var publication = await publicationSearch.Find(publicationId.ToString());

            if (publicationSearch.PublicationNotFound)
            {
                return NotFound(new PublicationNotFoundError(publicationId.ToString()));
            }

            return Ok(publication.MapToResponse());
        }

        /// <summary>
        /// Delete a publication
        /// </summary>
        /// <remarks>
        /// Delete a publication already registered.
        /// </remarks>
        /// <param name="publicationId" example="01FME0F949HAVJ91A9100N16ZS">Publication's ID</param>
        [HttpDelete, Route("{publicationId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PublicationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicationNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Ulid publicationId)
        {
            var publicationSearch = new PublicationSearch(_dbContext);
            var publication = await publicationSearch.Find(publicationId.ToString());

            if (publicationSearch.PublicationNotFound)
            {
                return NotFound(new PublicationNotFoundError(publicationId.ToString()));
            }

            var publicationDelete = new DeletePublication(_dbContext);
            await publicationDelete.Delete(publication);

            return Ok(publication.MapToResponse());
        }
    }
}
