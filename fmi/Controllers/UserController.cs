using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using fmi.Models;
using fmi.Services;
using Microsoft.AspNetCore.Identity;

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

                return Ok(result);
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
    }
}
