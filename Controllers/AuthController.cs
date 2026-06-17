using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SanblasBackend.Models;
using SanblasBackend.Services;
using SanblasBackend.Utils;

namespace SanblasBackend.Controllers;

public record TokenResponse(string Token);
public record UserCredential(string Email, string Password);

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IAuthenticationService authenticationService, IOptions<JwtSettings> jwtSettings)
    {
        _authenticationService = authenticationService;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserCredential user) 
    {
        var validUser = await _authenticationService.AuthenticateAsync(user.Email, user.Password);
        if (validUser is null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        var token = TokenGenerator.GenerateToken(validUser, _jwtSettings);

        return Ok(new TokenResponse(token));
    }
}
