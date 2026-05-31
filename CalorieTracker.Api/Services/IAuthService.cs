using CalorieTracker.Api.Dtos.Auths;

namespace CalorieTracker.Api.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}