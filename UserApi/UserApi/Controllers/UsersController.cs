using Microsoft.AspNetCore.Mvc;
using UserApi.Entities;
using UserApi.Services;
using Microsoft.AspNetCore;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace UserApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] CreateUserEntity model)
        {
            return await _userService.CreateAsync(model);
        } 
        
        [HttpPost("login"), Produces("application/json")]
        public async Task<IActionResult> Login()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                          throw new InvalidOperationException("OpenIddict request cannot be retrieved.");

            if (!request.IsPasswordGrantType() || request is not { Username: not null, Password: not null })
                return BadRequest(new { error = "unsupported_grant_type" });
            
            var principal = await _userService.LoginAsync(request.Username, request.Password, request.GetScopes());

            if (principal is null)
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
