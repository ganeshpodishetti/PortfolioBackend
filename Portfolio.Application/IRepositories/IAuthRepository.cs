using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.IRepositories;

public interface IAuthRepository
{
    Task<RegisterResponseDto> CreateUserAsync(RegisterRequestDto request);
    Task<LoginResponseDto> UpdateUserAsync(User user);
}