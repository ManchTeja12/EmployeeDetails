using EmployeeManage.Models;
using EmployeeManage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllers (IAuthService authservice): ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errors);
            }
            try
            {
                await authservice.Register(request);
                return Ok("User registered successfully");
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid email format");
            }
            catch (InvalidOperationException)
            {
                return Conflict("User already exists");
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(UserDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errors);
            }
            try
            {
                var token = await authservice.Login(request);
                return Ok(token);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password");
            }
        }

        [Authorize]
        [HttpGet("authenticated")]

        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }
    }
}
