using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.Api.Authorization;
using Portfolio.Api.Features.Users;
using Portfolio.Api.Models;
using Portfolio.Api.Models.Users;
using Portfolio.Domain.Users;

namespace Portfolio.Api.Controllers
{
    [Route("api/v1/login")]
    public class LoginControler : Controller
    {
        private readonly IUserRepository _userRepository;

        private readonly IOptions<JwtOptions> _jwtOptions;

        public LoginControler(IUserRepository userRepository, IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
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
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Login([FromBody] UserRequest userRequest)
        {
            var userSearch = new UserSearch(_userRepository);
            var user = await userSearch.Find(userRequest.Email);

            if (userSearch.UserNotFound)
            {
                return UnprocessableEntity(new ResponseError("USER_NOT_FOUND"));
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
