using Microsoft.AspNetCore.Identity;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Application.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Application.Services;

public class AuthService(IAuthRepository authRepository,
    UserManager<User> userManager,
    IJwtTokenService jwtTokenService) : IAuthService
{
    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            //_logger.LogError("Email already exists");
            throw new UserAlreadyExistsException(email: request.Email);
        }

        var response = await authRepository.CreateUserAsync(request);
        response.CreateAt = DateTime.UtcNow.ToString();
        return response;
    }

    // Login is used to authenticate the user and generate a JWT token and a refresh token.
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestRequest)
    {
        var existingUser = await userManager.FindByEmailAsync(loginRequestRequest.Email);

        if (existingUser == null || !await userManager.CheckPasswordAsync(existingUser, loginRequestRequest.Password))
        {
            throw new LoginFailedException(loginRequestRequest.Email);
        }

        var (jwtToken, expirationDateInUtc) = jwtTokenService.GenerateJwtToken(existingUser);
        var refreshTokenValue = jwtTokenService.GenerateRefreshToken();
        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);
        
        existingUser.RefreshToken = refreshTokenValue;
        existingUser.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;
        return await authRepository.UpdateUserAsync(existingUser, jwtToken, expirationDateInUtc);
    }
}
