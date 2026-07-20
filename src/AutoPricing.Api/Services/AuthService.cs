using AutoPricing.Api.Data;
using AutoPricing.Api.DTOs;
using AutoPricing.Api.Models;
using Microsoft.EntityFrameworkCore;
using AutoPricing.Api.Exceptions;

namespace AutoPricing.Api.Services;

public class AuthService
{
    private readonly VehicleDbContext _context;
    private readonly JwtService _jwtService;

    public AuthService(
        VehicleDbContext context,
        JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<int> RegisterAsync(RegisterUserDto dto)
    {
        var normalizedEmail = dto.Email
            .Trim()
            .ToLowerInvariant();

        var emailAlreadyExists = await _context.Users
            .AnyAsync(user => user.Email == normalizedEmail);

        if (emailAlreadyExists)
        {
            throw new ConflictException(
                "Já existe um usuário cadastrado com este e-mail.");
        }

        var passwordHash = BCrypt.Net.BCrypt
            .HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name.Trim(),
            Email = normalizedEmail,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var normalizedEmail = dto.Email
            .Trim()
            .ToLowerInvariant();

        var user = await _context.Users
            .FirstOrDefaultAsync(user =>
                user.Email == normalizedEmail);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "E-mail ou senha inválidos.");
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash);

        if (!passwordIsValid)
        {
            throw new UnauthorizedAccessException(
                "E-mail ou senha inválidos.");
        }

        return _jwtService.GenerateToken(user);
    }

}