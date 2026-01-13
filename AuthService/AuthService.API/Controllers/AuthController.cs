using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<ApplicationUser> UserManager , ITokenService TokenService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            Email = request.Email
        };

        var result = await UserManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await UserManager.AddToRoleAsync(user, request.Role);

        return Ok("Registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await UserManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized();

        var valid = await UserManager.CheckPasswordAsync(user, request.Password);
        if (!valid)
            return Unauthorized();

        var roles = await UserManager.GetRolesAsync(user);
        var token = TokenService.GenerateToken(user, roles);

        return Ok(new AuthResponse(token));
    }
}
