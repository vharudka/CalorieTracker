using CalorieTracker.Api.Dtos;
using CalorieTracker.Api.Helpers;
using CalorieTracker.Api.Repositories;

namespace CalorieTracker.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var salt = PasswordHelper.GenerateSalt();
            var hash = PasswordHelper.HashPassword(request.Password, salt);

            _ = await _repo.CreateUserAsync(request.Email, hash, salt);

            return new RegisterResponse("Registration successful. You can now log in.");
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _repo.GetUserByEmailAsync(request.Email) ?? throw new Exception("Invalid credentials");

            var valid = PasswordHelper.Verify(request.Password, user.PasswordSalt, user.PasswordHash);
            if (!valid)
            {
                throw new Exception("Invalid credentials");
            }

            var token = JwtHelper.GenerateToken(user.Id.ToString(), request.Email, _config);

            return new LoginResponse(token, new UserDto(user.Id, user.Email));
        }
    }
}