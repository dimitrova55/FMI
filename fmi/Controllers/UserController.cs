using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using fmi.Models;
using fmi.Services;



namespace fmi.Controllers
{
    [Route("fmi/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }


        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                var result = await userService.Register(userDto);

                if (!result.Success)
                {
                    return StatusCode(result.StatusCode, result.Message);
                }

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            try
            {
                var result = await userService.Login(userDto);

                if (!result.Success)
                {
                    return StatusCode(result.StatusCode, result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("google-signin")]
        public async Task<IActionResult> GoogleSignin([FromBody] string googleToken)
        {
            try
            {
                var token = await userService.AuthenticateGoogleUser(googleToken);
                
                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
