using CalorieTracker.Api.Dtos.Auths;

namespace CalorieTracker.Api.Services.Auths;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}