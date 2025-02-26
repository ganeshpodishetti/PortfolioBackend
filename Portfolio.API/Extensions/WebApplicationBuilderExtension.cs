using Microsoft.OpenApi.Models;

namespace Portfolio.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi("v1",options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "Restaurants Catalog API",
                    Version = "v1",
                    Description = "Modern API for managing restaurant catalogs.",
                };
                return Task.CompletedTask;
            });
        });
    }
}