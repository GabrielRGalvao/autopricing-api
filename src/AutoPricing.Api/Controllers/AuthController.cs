using AutoPricing.Api.DTOs;
using AutoPricing.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoPricing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var userId = await _authService.RegisterAsync(dto);

        return Created(
            $"/api/auth/users/{userId}",
            new
            {
                id = userId,
                message = "Usuário cadastrado com sucesso."
            });
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        return Ok(new
        {
            token,
            message = "Login realizado com sucesso."
        });
    }
}