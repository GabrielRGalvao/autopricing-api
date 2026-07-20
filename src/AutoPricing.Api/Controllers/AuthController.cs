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
}