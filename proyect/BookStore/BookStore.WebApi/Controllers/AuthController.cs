using BookStore.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.WebApi.Controllers
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
        [HttpPost("Token")]
        public IActionResult GetToken([FromBody] Auth credentials)
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

            return Ok( Security.JwtTokenBuilder.WriteToken(token) );
        }
    }
}