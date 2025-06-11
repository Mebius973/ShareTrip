using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Entities;
using UserApi.Repositories;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] UserEntity model)
        {
            
            return await _userService.CreatAsync(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserEntity model)
        {
            var user = await _userService.LoginAsync(model);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("TA_CLEF_SUPER_SECRETE_à_ne_pas_commit");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    Issuer = "https://tonapp.com",
                    Audience = "https://tonapp.com",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized();
        }

    }
}
