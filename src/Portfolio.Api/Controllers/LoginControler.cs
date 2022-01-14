using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.Api.Authorization;
using Portfolio.Api.Features.Users;
using Portfolio.Api.Infrastructure.Database.DataModel.Users;
using Portfolio.Api.Models.Users;
using Portfolio.Api.Infrastructure.Serialization.Users;

namespace Portfolio.Api.Controllers
{
    [Route("Login")]
    public class LoginControler : Controller
    {
        private readonly IDynamoDBContext _dbContext;

        private readonly IOptions<JwtOptions> _jwtOptions;

        public LoginControler(IDynamoDBContext dbContext, IOptions<JwtOptions> jwtOptions)
        {
            _dbContext = dbContext;
            _jwtOptions = jwtOptions;
        }

        /// <summary>
        /// User's Login
        /// </summary>
        /// <remarks>
        /// Create a Login From User's Credendentials
        /// </remarks>
        [HttpPut, AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserNotFoundError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login([FromBody] UserRequest userRequest)
        {
            var userSearch = new UserSearch(_dbContext);
            var user = await userSearch.Find(userRequest.Email);

            if (userSearch.UserNotFound)
            {
                return NotFound(new UserNotFoundError());
            }

            if (!BCrypt.Net.BCrypt.Verify(userRequest.Password, user.Password))
            {
                return Forbid();
            }

            var apiToken = new ApiToken(_jwtOptions);
            apiToken.User = user;

            return Ok(new { Token = apiToken.ToString(), User = user.MapToResponse() });
        }
    }
}
