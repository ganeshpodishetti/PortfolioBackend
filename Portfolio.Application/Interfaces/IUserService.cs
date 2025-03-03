using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces;

public interface IUserService
{ 
    Task<UserResponseDto> GetUserByEmailAsync(string email); 
}