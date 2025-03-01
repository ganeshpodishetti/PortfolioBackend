using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfolio.API.Handlers;
using Portfolio.Domain.Contracts;
using Portfolio.Domain.Entities;
using WatchDog;
using WatchDog.src.Enums;

namespace Portfolio.API.Extensions;

public static partial class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi("v1",options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "Portfolio API",
                    Version = "v1",
                    Description = "Modern API for Portfolio."
                };
                return Task.CompletedTask;
            });
        });
        
        // Add JWT settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.JwtSettingsKey));

        // Add WatchDog services
        builder.Services.AddWatchDogServices(options =>
        {
            options.IsAutoClear = true;
            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
        });
        
        // Watchdog logger
        builder.Logging.AddWatchDogLogger();
        
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddIdentityApiEndpoints<User>();
    }
    
    // Config JWT Authentication
    public static void ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        //Checking JWT key
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        //var jwtSettings = configuration.GetSection("JwtSettings");
        if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
        {
            throw new InvalidOperationException("JWT secret key is not configured.");
        }

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            message = "You are not authorized to access this resource. Please authenticate."
                        });
                        return context.Response.WriteAsync(result);
                    },
                };
            });
    }
}
