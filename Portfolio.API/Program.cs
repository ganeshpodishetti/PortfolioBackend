using Portfolio.API.Extensions;
using Portfolio.Application.Extension;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Extension;
using Scalar.AspNetCore;
using WatchDog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.ConfigureJwtAuth(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.AddPresentation();
    builder.Services.AddApplication();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("Portfolio API")
                .WithTheme(ScalarTheme.Mars)
                .WithDarkMode(true)
                .WithSidebar(true)
                .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.Http11)
                .WithPreferredScheme("Bearer");
        });
    }

    // Exception Handler
    app.UseExceptionHandler(_ => { });

    // WatchDog middleware
    app.UseWatchDogExceptionLogger();
    app.UseWatchDog(opt =>
    {
        opt.WatchPageUsername = builder.Configuration["WatchDogUserName"];
        opt.WatchPagePassword = builder.Configuration["WatchDogPassword"];
    });

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors("AllowAll");

    //app.UseAuthentication(); invoked by calling MapIdentityApi<User>
    // app.MapGroup("api/user")
    //     .WithTags("User")
    //     .MapIdentityApi<User>();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{
    throw new Exception("An error occurred while start-up the application.");
}
