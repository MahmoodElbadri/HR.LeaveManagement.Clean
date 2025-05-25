using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var authresponse = await _authService.Login(request);
        return Ok(authresponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var authResponse = await _authService.Register(request);
        return Ok(authResponse);
    }
}
