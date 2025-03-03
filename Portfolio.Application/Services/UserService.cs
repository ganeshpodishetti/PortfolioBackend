using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Application.IRepositories;
using Portfolio.Domain.Entities;
using Exception = System.Exception;

namespace Portfolio.Application.Services;

public class UserService(UserManager<User> userManager,
    IUserRepository userRepository,
    IMapper mapper) : IUserService
{
    // This method is used to get a user by email
    public async Task<UserResponseDto> GetUserByEmailAsync(string email)
    {
        var userById = await userManager.FindByEmailAsync(email);
        if (userById == null)
        {
            throw new Exception("User not found");
        }
        var user = await userRepository.GetByEmailAsync(userById.Email!);
        var responseDto = mapper.Map<UserResponseDto>(user);
        return responseDto;
    }
}