using Microsoft.AspNetCore.Mvc;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;

namespace OnlineShopWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            string authToken = _authenticationService.Login(loginDto);
            if (authToken != null)
            {
                if (authToken == string.Empty)
                {
                    return BadRequest("Wrong password!");

                }

                return Ok(authToken);
            }
            else
            {
                return BadRequest("Wrong email!");
            }
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto regModel)
        {
            try
            {
                _authenticationService.Register(regModel);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("socialLogin")]
        public IActionResult SocialLogin([FromBody] UserDto socialLoginModel)
        {
            var token = _authenticationService.SocialLogin(socialLoginModel);

            if (token != null)
            {
                if (token == string.Empty)
                {
                    return BadRequest("Wrong password!");

                }
                return Ok(token);
            }
            else
            {
                return BadRequest("Wrong email!");
            }
        }

    }
}
