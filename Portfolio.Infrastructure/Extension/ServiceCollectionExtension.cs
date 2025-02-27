using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Context;

namespace Portfolio.Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Registering the PortfolioDbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PortfolioDbContext>(options => options.UseNpgsql(connectionString)
            .EnableSensitiveDataLogging());

        // Registering the Identity Services
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<PortfolioDbContext>();
    }
}