using Microsoft.EntityFrameworkCore;
using Portfolio.Application.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Context;

namespace Portfolio.Infrastructure.Repositories;

internal class UserRepository(PortfolioDbContext portfolioDbContext) : IUserRepository
{
    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var user = await portfolioDbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        return user;
    }
}