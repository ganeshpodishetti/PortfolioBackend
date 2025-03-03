using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("getUserByEmail")]
    public async Task<IActionResult> GetUserByEmail([FromQuery]string email)
    {
        var response = await userService.GetUserByEmailAsync(email);
        return Ok(response);
    }
}