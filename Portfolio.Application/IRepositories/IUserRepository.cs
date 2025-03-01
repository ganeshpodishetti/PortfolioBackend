using Portfolio.Domain.Entities;

namespace Portfolio.Application.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
}