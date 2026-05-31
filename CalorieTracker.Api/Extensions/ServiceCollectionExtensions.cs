using CalorieTracker.Api.Repositories;
using CalorieTracker.Api.Services;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace CalorieTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IDbConnection>(_ =>
            new SqlConnection(config.GetConnectionString("Default"))
        );

        return services;
    }

    public static IServiceCollection AddFrontendCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthentication
    (
        this IServiceCollection services,
        IConfiguration config
    )
    {
        var jwtSection = config.GetSection("Jwt");

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSection["Key"])
                    )
                };
            });

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFoodCacheService, FoodCacheService>();
        services.AddScoped<IFoodEntriesService, FoodEntriesService>();
        services.AddScoped<IUserGoalsService, UserGoalsService>();
        services.AddScoped<IStatsService, StatsService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFoodCacheRepository, FoodCacheRepository>();
        services.AddScoped<IFoodEntriesRepository, FoodEntriesRepository>();
        services.AddScoped<IUserGoalsRepository, UserGoalsRepository>();
        services.AddScoped<IStatsRepository, StatsRepository>();

        return services;
    }
}