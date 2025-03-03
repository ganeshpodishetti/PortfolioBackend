using Portfolio.API.Extensions;
using Portfolio.Application.Extension;
using Portfolio.Infrastructure.Extension;
using Scalar.AspNetCore;
using WatchDog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.AddPresentation();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddJwtAuthentication(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("Portfolio API")
                .WithTheme(ScalarTheme.Mars)
                .WithDarkMode(true)
                .WithSidebar(true)
                .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.Http11)
                .Authentication = new ScalarAuthenticationOptions()
                {
                    PreferredSecurityScheme = "Bearer",
                };
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

    app.UseCors("AllowAll");

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication(); // Must come before UseAuthorization
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception)
{
    throw new Exception("An error occurred while start-up the application.");
}
