using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Portfolio.Api.Models.Papers;
using Portfolio.Api.Infrastructure.Database.DataModel.Papers;
using Portfolio.Api.Infrastructure.Serialization.Papers;
using Portfolio.Api.Features.Papers;
using NUlid;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Api.Controllers
{
    [Route("Papers")]
    public class PapersController : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        public PapersController(IDynamoDBContext dBContext)
        {
            _dbContext = dBContext;
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
            var query = new PaperQuery();
            query.Title = queryString.Title;
            query.SubmissionDeadline = queryString.SubmissionDeadline;
            query.Qualis = queryString.Qualis;
            query.Type = queryString.Type;

            var papers = await _dbContext
                .FromQueryAsync<Paper>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            return Ok(new GetPaperResponse
            {
                Papers = papers.Select(paper => paper.MapToResponse())
            });
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
            var createPaper = new CreatePaper(_dbContext);
            var paper = paperRequest.ToPaper();

            await createPaper.Register(paper);

            return Ok(paper.MapToResponse());
        }

        /// <summary>
        /// Update a paper
        /// </summary>
        /// <remarks>
        /// Update a paper already registered.
        /// </remarks>
        /// <param name="paperId" example="01FME0F949HAVJ91A9100N16ZS">Paper's ID</param>
        /// <param name="putPaperRequest">Paper's content</param>
        [HttpPut, Route("{paperId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PaperNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] Ulid paperId, [FromBody] PutPaperRequest putPaperRequest)
        {
            var paperSearch = new PaperSearch(_dbContext);
            var paper = await paperSearch.Find(paperId.ToString());

            if (paperSearch.PaperNotFound)
            {
                return NotFound(new PaperNotFoundError(paperId.ToString()));
            }

            putPaperRequest.MapTo(paper);

            var paperUpdate = new PaperUpdate(_dbContext);
            await paperUpdate.Update(paper);

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
        [ProducesResponseType(typeof(PaperNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] Ulid paperId)
        {
            var paperSearch = new PaperSearch(_dbContext);
            var paper = await paperSearch.Find(paperId.ToString());

            if (paperSearch.PaperNotFound)
            {
                return NotFound(new PaperNotFoundError(paperId.ToString()));
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
        [ProducesResponseType(typeof(PaperNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Ulid paperId)
        {
            var paperSearch = new PaperSearch(_dbContext);
            var paper = await paperSearch.Find(paperId.ToString());

            if (paperSearch.PaperNotFound)
            {
                return NotFound(new PaperNotFoundError(paperId.ToString()));
            }

            var paperDelete = new DeletePaper(_dbContext);
            await paperDelete.Delete(paper);

            return Ok(paper.MapToResponse());
        }
    }
}
