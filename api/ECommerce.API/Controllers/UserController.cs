using ECommerce.API.DTOs;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserSerivces _userSerivces;
        public UserController(ILogger<UserController> logger, UserSerivces userSerivces)
        {
            _logger = logger;
            _userSerivces = userSerivces;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO dto)
        {
            var result = await _userSerivces.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _userSerivces.LoginAsync(dto, ip);
            return Ok(result);
        }
    }
}
