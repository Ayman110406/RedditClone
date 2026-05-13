using Microsoft.AspNetCore.Mvc;
using RedditClone.Dtos;
using RedditClone.Services;

namespace RedditClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService authService)
        {
            _service = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto regDTO)
        {

            if (await _service.RegisterUserAsync(regDTO))
            {
                return Ok("Gebruiker geregistreerd");
            }
            else
            {
                return BadRequest("Registreren mislukt");
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            var authResponse = await _service.LoginUserAsync(loginDTO);
            if (authResponse != null)
            {
                return Ok(authResponse);
            }
            else
            {
                return Unauthorized("Inloggen mislukt");
            }

        }
    }
}
