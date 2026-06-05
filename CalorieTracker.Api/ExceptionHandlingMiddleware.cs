using CalorieTracker.Api.Exceptions;

namespace CalorieTracker.Api;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case UsernameAlreadyExistsException:
                    _logger.LogWarning(ex, ex.Message);
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;

                case UserGoalsNotFoundException:
                case FoodEntryNotFoundException:
                    _logger.LogWarning(ex, ex.Message);
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                case InvalidCredentialsException:
                    _logger.LogWarning(ex, ex.Message);
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;

                default:
                    _logger.LogError(ex, "Unhandled exception");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var result = new
            {
                error = ex.Message
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}