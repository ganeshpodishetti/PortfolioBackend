using Microsoft.OpenApi.Models;
using Portfolio.API.Handlers;
using Portfolio.Domain.Contracts;
using WatchDog;
using WatchDog.src.Enums;

namespace Portfolio.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
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
        
        // Add Identity API endpoints
        //builder.Services.AddIdentityApiEndpoints<User>();
    }
}
