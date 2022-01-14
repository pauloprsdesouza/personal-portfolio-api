using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUlid;
using Portfolio.Api.Infrastructure.Database.DataModel.Posts;
using Portfolio.Api.Models.Posts;
using System.Linq;
using Portfolio.Api.Infrastructure.Serialization.Posts;
using Portfolio.Api.Features.Posts;
using Portfolio.Api.Infrastructure.Serialization.Users;
using Portfolio.Api.Features.Users;
using Portfolio.Api.Infrastructure.Database.DataModel.Categories;
using System.Collections.Generic;
using Portfolio.Api.Infrastructure.Serialization.Categories;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Api.Controllers
{
    [Route("Posts")]
    public class PostsController : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        public PostsController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
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
            var query = new PostQuery();
            query.BeforePost = queryString.Before ?? Ulid.NewUlid();
            query.Status = "P";
            query.Title = queryString.Title;
            query.Length = queryString.Length ?? 30;
            query.CategoryId = queryString.CategoryId;

            var userSearch = new UserSearch(_dbContext);
            var user = await userSearch.Find(UserEmail);

            var posts = await _dbContext
                .FromQueryAsync<Post>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            IEnumerable<PostResponse> postsResponse = posts.Select(post => post.MapToResponse()).ToList();

            foreach (var post in postsResponse)
            {
                post.PostedBy = user.MapToResponse();
            };

            return Ok(new GetPostResponse
            {
                Posts = postsResponse
            });
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
            var query = new PostQuery();
            query.BeforePost = queryString.Before ?? Ulid.NewUlid();
            query.Status = queryString.Status;
            query.Length = queryString.Length ?? 30;
            query.CategoryId = queryString.CategoryId;

            var userSearch = new UserSearch(_dbContext);
            var user = await userSearch.Find(UserEmail);

            var posts = await _dbContext
                .FromQueryAsync<Post>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            IEnumerable<PostResponse> postsResponse = posts.Select(post => post.MapToResponse()).ToList();

            foreach (var post in postsResponse)
            {
                post.PostedBy = user.MapToResponse();
            };

            return Ok(new GetPostResponse
            {
                Posts = postsResponse
            });
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
            var createPost = new CreatePost(_dbContext);
            var post = postRequest.ToPost();

            await createPost.Register(UserId, UserEmail, post);

            return Ok(post.MapToResponse());
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        /// <remarks>
        /// Update a post already registered.
        /// </remarks>
        /// <param name="postId" example="01FME0F949HAVJ91A9100N16ZS">Posts's ID</param>
        /// <param name="putPostRequest">Posts's content</param>
        [HttpPut, Route("{postId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PostNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] Ulid postId, [FromBody] PutPostRequest putPostRequest)
        {
            var postSearch = new PostSearch(_dbContext);
            var post = await postSearch.Find(postId);

            if (postSearch.PostNotFound)
            {
                return NotFound(new PostNotFoundError(postId.ToString()));
            }

            putPostRequest.MapTo(post);

            var postUpdate = new PostUpdate(_dbContext);
            await postUpdate.Update(post);

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
        [ProducesResponseType(typeof(PostNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateViews([FromRoute] Ulid postId)
        {
            var postSearch = new PostSearch(_dbContext);
            var post = await postSearch.Find(postId);

            if (postSearch.PostNotFound)
            {
                return NotFound(new PostNotFoundError(postId.ToString()));
            }

            var views = int.Parse(post.Views) + 1;
            post.Views = views.ToString();

            var postUpdate = new PostUpdate(_dbContext);
            await postUpdate.Update(post);

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
        [ProducesResponseType(typeof(PostNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Find([FromRoute] Ulid postId)
        {
            var postSearch = new PostSearch(_dbContext);
            var post = await postSearch.Find(postId);

            if (postSearch.PostNotFound)
            {
                return NotFound(new PostNotFoundError(postId.ToString()));
            }

            var userSearch = new UserSearch(_dbContext);
            var user = await userSearch.Find(UserEmail);

            var query = new CategoryQuery();

            var categories = await _dbContext
                .FromQueryAsync<Category>(query.ToDynamoDBQuery())
                .GetRemainingAsync();

            PostResponse postResponse = post.MapToResponse();
            postResponse.PostedBy = user.MapToResponse();
            postResponse.Category = categories
                                        .Where(category => category.Id.ToString() == post.CategoryId)
                                        .FirstOrDefault()
                                        .MapToResponse();

            return Ok(postResponse);
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
        [ProducesResponseType(typeof(PostNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Ulid postId)
        {
            var postSearch = new PostSearch(_dbContext);
            var post = await postSearch.Find(postId);

            if (postSearch.PostNotFound)
            {
                return NotFound(new PostNotFoundError(postId.ToString()));
            }

            var postDelete = new DeletePost(_dbContext);
            await postDelete.Delete(post);

            return Ok(post.MapToResponse());
        }
    }
}
