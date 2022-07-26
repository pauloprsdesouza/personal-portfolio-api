using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Portfolio.Api.Models.Papers;
using Portfolio.Api.Features.Papers;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Domain.Papers;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers
{
    [Route("api/v1/papers")]
    public class PapersController : Controller
    {
        private readonly IPaperRepository _paperRepository;

        public PapersController(IPaperRepository paperRepository)
        {
            _paperRepository = paperRepository;
        }

        /// <summary>
        /// Get papers
        /// </summary>
        /// <remarks>
        /// Registered Papers.
        /// </remarks>
        [HttpGet, AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetPaperResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> List([FromQuery] GetPapersQuery queryString)
        {
            return Ok();
        }

        /// <summary>
        /// Create a paper
        /// </summary>
        /// <remarks>
        /// Create a paper, which can be a journal or conference.
        /// </remarks>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaperResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] PaperRequest paperRequest)
        {
            var paperRegistration = new PaperRegistration(_paperRepository);
            var paper = paperRequest.ToPaper();

            await paperRegistration.Register(paper);

            return Ok(paper.MapToResponse());
        }

        /// <summary>
        /// Update a paper
        /// </summary>
        /// <remarks>
        /// Update a paper already registered.
        /// </remarks>
        /// <param name="paperId" example="01FME0F949HAVJ91A9100N16ZS">Paper's ID</param>
        /// <param name="paperRequest">Paper's content</param>
        [HttpPut, Route("{paperId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Update([FromRoute] int paperId, [FromBody] PutPaperRequest paperRequest)
        {
            var paperUpdate = new PaperUpdate(_paperRepository);
            var paper = await paperUpdate.Update(paperId, paperRequest);

             if (paperUpdate.PaperNotFound)
            {
                return UnprocessableEntity(new ResponseError("PAPER_NOT_FOUND"));
            }

            return Ok(paper.MapToResponse());
        }

        /// <summary>
        /// Find a paper
        /// </summary>
        /// <remarks>
        /// Find a paper already registered.
        /// </remarks>
        /// <param name="paperId" example="01FME0F949HAVJ91A9100N16ZS">Paper's ID</param>
        [HttpGet, Route("{paperId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] int paperId)
        {
            var paperSearch = new PaperSearch(_paperRepository);
            var paper = await paperSearch.Find(paperId);

            if (paperSearch.PaperNotFound)
            {
                return UnprocessableEntity(new ResponseError("PAPER_NOT_FOUND"));
            }

            return Ok(paper.MapToResponse());
        }

        /// <summary>
        /// Delete a paper
        /// </summary>
        /// <remarks>
        /// Delete a paper already registered.
        /// </remarks>
        /// <param name="paperId" example="01FME0F949HAVJ91A9100N16ZS">Paper's ID</param>
        [HttpDelete, Route("{paperId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] int paperId)
        {
            var paperRemoval = new PaperRemoval(_paperRepository);
            var paper = await paperRemoval.Delete(paperId);

            if (paperRemoval.PaperNotFound)
            {
                return UnprocessableEntity(new ResponseError("PAPER_NOT_FOUND"));
            }

            return Ok(paper.MapToResponse());
        }
    }
}
