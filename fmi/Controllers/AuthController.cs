using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using fmi.Models;
using fmi.Services;
using Microsoft.AspNetCore.Identity;

namespace fmi.Controllers
{
    [Route("fmi/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                var result = await authService.Register(userDto);

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
                var result = await authService.Login(userDto);

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
