using Microsoft.AspNetCore.Mvc;
using Polls.Api.Service.User;
using SabeloSethu.Api.Models.User;

namespace Polls.Api.Controllers
{
    [Route("api/[controller]")]

    public class AuthController : Controller
    {
        private readonly IUserService userService;
        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(user);
            }
            var registerUser = await userService.RegisterUser(user);

            if (registerUser.IsSuccess)
            {
                var tokenResponse = await userService.GetToken(new UserLoginModel
                {
                    Username = user.Email,
                    Password = user.Password
                });

                if (tokenResponse.IsSuccess)
                {
                    return Ok(tokenResponse);
                }
                else
                {
                    return Unauthorized(tokenResponse);
                }
            }
            else
            {
                return BadRequest(registerUser);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetToken([FromBody] UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { user });
            }

            var tokenResponse = await userService.GetToken(user);

            if (tokenResponse.IsSuccess)
            {
                return Ok(tokenResponse);

            }

            return Unauthorized(tokenResponse);
        }
    }
}
