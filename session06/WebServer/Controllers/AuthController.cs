using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebServer.Security;
using WebServer.ViewModels;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/Auth
        [HttpPost]
        public IActionResult GetToken([FromBody] UserViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (credentials.UserName != "admin")
            {
                return Unauthorized();
            }

            var token = Security.JwtTokenBuilder.GetSecuredToken(Configuration);

            return Ok(new {
                Token = Security.JwtTokenBuilder.WriteToken(token) 
            });
        }
    }
}
