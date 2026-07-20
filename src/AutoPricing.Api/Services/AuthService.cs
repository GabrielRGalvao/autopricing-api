using AutoPricing.Api.Data;
using AutoPricing.Api.DTOs;
using AutoPricing.Api.Models;
using Microsoft.EntityFrameworkCore;
using AutoPricing.Api.Exceptions;

namespace AutoPricing.Api.Services;

public class AuthService
{
    private readonly VehicleDbContext _context;

    public AuthService(VehicleDbContext context)
    {
        _context = context;
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
}