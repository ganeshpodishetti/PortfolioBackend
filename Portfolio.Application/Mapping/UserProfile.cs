using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, LoginResponseDto>();
        CreateMap<User, RegisterResponseDto>();
        CreateMap<RegisterRequestDto, User>();
        CreateMap<LoginRequestDto, User>();
    }
}