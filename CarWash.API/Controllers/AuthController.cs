using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.BL.DTOs;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterUserAsync(registerDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.ValidateUserAsync(loginDto);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}