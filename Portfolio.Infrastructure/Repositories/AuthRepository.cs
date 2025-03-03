using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Application.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Infrastructure.Repositories;

internal class AuthRepository(UserManager<User> userManager,
    IJwtTokenService jwtTokenService,
    IMapper mapper) : IAuthRepository
{
    // register a new user
    public async Task<RegisterResponseDto> CreateUserAsync(RegisterRequestDto request)
    {
        var newUser = mapper.Map<User>(request);

        // Generate a unique username
        newUser.UserName = request.Email;
        //newUser.UserName = GenerateUserName(request.FirstName, request.LastName);
        newUser.PasswordHash = userManager.PasswordHasher.HashPassword(newUser, request.Password);
        var result = await userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            //_logger.LogError("Failed to create user: {errors}", errors);
            throw new RegistrationFailedException(result.Errors.Select(err => err.Description));
        }
        // _logger.LogInformation("User created successfully");
        await jwtTokenService.GenerateJwtToken(newUser);
        newUser.CreatedAt = DateTime.UtcNow;
        return mapper.Map<RegisterResponseDto>(newUser);
    }

    // update the user with the new JWT token and expiry date
    public async Task<LoginResponseDto> UpdateUserAsync(User user)
    {
        var jwtToken = await jwtTokenService.GenerateJwtToken(user);
        var refreshToken = jwtTokenService.GenerateRefreshToken();

        // Hash the refresh token and store it in the database or override the existing refresh token
        //var refreshTokenHash = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));
        //user.RefreshToken = Convert.ToBase64String(refreshTokenHash);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(2);
        
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            //_logger.LogError("Failed to update user: {errors}", errors);
            throw new Exception($"Failed to update user: {errors}");
        }

        var response = mapper.Map<User, LoginResponseDto>(user);
        response.AccessToken = jwtToken;
        response.RefreshToken = refreshToken;
        return response;
    }
}