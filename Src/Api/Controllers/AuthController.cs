using access_manager_api.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

namespace access_manager_api.Api.Controllers;

[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return Ok(ApiResponse<string>.SuccessResponse("Login endpoint"));
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(ApiResponse<string>.SuccessResponse("Logout endpoint"));
    }
}