using CalorieTracker.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddOpenApi();

services.AddFrontendCors();
services.AddJwtAuthentication(builder.Configuration);
services.AddDatabase(builder.Configuration);
services.AddServices();
services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ❗ IMPORTANT: Disable HTTPS redirection for local dev
// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();