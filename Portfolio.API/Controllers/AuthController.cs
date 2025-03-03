using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;


[Route("api/auth")]
[ApiController]
[Authorize]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var response = await authService.RegisterAsync(request);
        return Ok(response);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await authService.LoginAsync(request);
        return Ok(response);
    }
}