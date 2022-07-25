using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Models.Posts;
using Portfolio.Api.Features.Posts;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Domain.Posts;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers
{
    [Route("api/v1/posts")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        const string UserId = "01FPSM8KW4ZPRS2K7JJHM3WSJZ";
        const string UserEmail = "paulo.prsdesouza@gmail.com";

        /// <summary>
        /// Get posts from Anonymous Request.
        /// </summary>
        /// <remarks>
        /// Get registered posts.
        /// </remarks>
        [HttpGet, Route("published"), AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetPostResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> ListPublished([FromQuery] GetPostsQuery queryString)
        {
            return Ok();
        }

        /// <summary>
        /// Get posts.
        /// </summary>
        /// <remarks>
        /// Get registered posts.
        /// </remarks>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetPostResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> List([FromQuery] GetPostsQuery queryString)
        {
            return Ok();
        }

        /// <summary>
        /// Create a post.
        /// </summary>
        /// <remarks>
        /// Save a post.
        /// </remarks>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] PostRequest postRequest)
        {
            var createPost = new PostRegistration(_postRepository);
            var post = postRequest.ToPost();

            await createPost.Register(post);

            return Ok(post.MapToResponse());
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        /// <remarks>
        /// Update a post already registered.
        /// </remarks>
        /// <param name="postId" example="01FME0F949HAVJ91A9100N16ZS">Posts's ID</param>
        /// <param name="postRequest">Posts's content</param>
        [HttpPut, Route("{postId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Update([FromRoute] int postId, [FromBody] PutPostRequest postRequest)
        {
            var postUpdate = new PostUpdate(_postRepository);
            var post = await postUpdate.Update(postId, postRequest);

            if (postUpdate.PostNotFound)
            {
                return UnprocessableEntity(new ResponseError("POST_NOT_FOUND"));
            }

            return Ok(post.MapToResponse());
        }

        /// <summary>
        /// Update views of a post.
        /// </summary>
        /// <remarks>
        /// Update a post already registered.
        /// </remarks>
        /// <param name="postId" example="01FME0F949HAVJ91A9100N16ZS">Post's ID</param>
        [HttpPut, Route("views/{postId}"), AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> UpdateViews([FromRoute] int postId)
        {
            var postUpdate = new PostViewsUpdate(_postRepository);
            var post = await postUpdate.Update(postId);

            if (postUpdate.PostNotFound)
            {
                return UnprocessableEntity(new ResponseError("POST_NOT_FOUND"));
            }

            return Ok(post.MapToResponse());
        }

        /// <summary>
        /// Find a post.
        /// </summary>
        /// <remarks>
        /// Find a post already registerd.
        /// </remarks>
        /// <param name="postId" example="01FME0F949HAVJ91A9100N16ZS">Posts's ID</param>
        [HttpGet, Route("{postId}"), AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] int postId)
        {
            var postSearch = new PostSearch(_postRepository);
            var post = await postSearch.Find(postId);

            if (postSearch.PostNotFound)
            {
                return NotFound(new ResponseError("POST_NOT_FOUND"));
            }

            return Ok(post.MapToResponse());
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <remarks>
        /// Delete a post already registered.
        /// </remarks>
        /// <param name="postId" example="01FME0F949HAVJ91A9100N16ZS">Posts's ID</param>
        [HttpDelete, Route("{postId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Archivement([FromRoute] int postId)
        {
            var postArchivement = new PostArchivement(_postRepository);
            var post = await postArchivement.Delete(postId);

            if (postArchivement.PostNotFound)
            {
                return UnprocessableEntity(new ResponseError("POST_NOT_FOUND"));
            }

            return Ok(post.MapToResponse());
        }
    }
}
