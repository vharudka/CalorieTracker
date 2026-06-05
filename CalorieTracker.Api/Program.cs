using CalorieTracker.Api;
using CalorieTracker.Api.Extensions;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddCacheOptions(config);
services.AddValidatorsFromAssemblyContaining<Program>();
services.AddHttpClient();
services.AddMemoryCache();
services.AddControllers();
services.AddOpenApi();

services.AddFrontendCors();
services.AddJwtAuthentication(config);
services.AddDatabase(config);
services.AddServices();
services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

// ❗ IMPORTANT: Disable HTTPS redirection for local dev
// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();