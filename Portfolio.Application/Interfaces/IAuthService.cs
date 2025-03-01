using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    //Task<CurrentUserResponse> GetCurrentUserAsync();
    //Task<UserResponseDto> GetByIdAsync(Guid id);
    //Task<UserResponseDto> UpdateAsync(Guid id, UpdateUserRequest request);
    //Task DeleteAsync(Guid id);
    //Task<RevokeRefreshTokenResponse> RevokeRefreshToken(RefreshTokenRequest refreshTokenRemoveRequest);
    //Task<CurrentUserResponse> RefreshTokenAsync(RefreshTokenRequest request);
}