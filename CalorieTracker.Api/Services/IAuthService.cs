using CalorieTracker.Api.Dtos;

namespace CalorieTracker.Api.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse> Register(RegisterRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
