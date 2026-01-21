using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace fmi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;
        private readonly IConfiguration _configuration;

    }
}
