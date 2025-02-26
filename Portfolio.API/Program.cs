using Portfolio.API.Extensions;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Extension;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Restaurant API")
            .WithTheme(ScalarTheme.Mars)
            .WithDarkMode(true)
            .WithSidebar(true)
            .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.Http11)
            .Authentication = new ScalarAuthenticationOptions
            {
                PreferredSecurityScheme = "Bearer"
            };
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthentication(); invoked by calling MapIdentityApi<User>
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();