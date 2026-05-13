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
            var result = await _service.RegisterUserAsync(regDTO);
            if(!result.Success){
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            var result = await _service.LoginUserAsync(loginDTO);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            return Ok(result);

        }
    }
}
