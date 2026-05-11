using EmployeeManage.Dtos;
using EmployeeManage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllers(IAuthService authservice) : ControllerBase


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
                ApiResponse apiResponse = new ApiResponse
                {
                    success = true,
                    message = "User registered successfully",
                    data = request
                };
                return Ok(apiResponse);
            }
            catch (ArgumentException)
            {
                ApiResponse apiResponse = new ApiResponse
                {
                    success = false,
                    message = "Invalid email format"
                };
                return BadRequest(apiResponse);
            }
            catch (InvalidOperationException)
            {
                ApiResponse apiResponse = new ApiResponse
                {
                    success = false,
                    message = "User already exists"
                    
                };
                return Conflict(apiResponse);
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
                var userResponse = await authservice.Login(request);
        ApiResponse apiResponse = new ApiResponse
        {
          success = true,
          message = "Login successful",
          data = userResponse
        };
                return Ok(apiResponse);
            }
            catch (KeyNotFoundException)
            {
                ApiResponse apiResponse = new ApiResponse
                {
                    success = false,
                    message = "User not found"
                };
                return NotFound(apiResponse);
            }
            catch (UnauthorizedAccessException)
            {
                ApiResponse apiResponse = new ApiResponse
                {
                    success = false,
                    message = "Invalid email or password"
                    
                };
                return Unauthorized(apiResponse);
            }
        }

        [Authorize]
        [HttpGet("authenticated")]

        public IActionResult AuthenticatedOnlyEndpoint()
        {
            ApiResponse apiResponse = new ApiResponse
            {
                success = true,
                message = "You are authenticated"
               // data = null
            };
            return Ok(apiResponse);
        }
    }
}
