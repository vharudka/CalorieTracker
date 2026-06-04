using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Helpers;
using CalorieTracker.Api.Repositories.Users;

namespace CalorieTracker.Api.Services.Auths;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _repository.GetByUsernameAsync(request.Username);
        if (existing is not null)
        {
            throw new Exception("Username already registered");
        }

        var salt = PasswordHelper.GenerateSalt();
        var hash = PasswordHelper.HashPassword(request.Password, salt);

        var user = await _repository.CreateAsync(request.Username, hash, salt);
        var token = JwtHelper.GenerateToken(user, _config);

        return new AuthResponse(user.Id, user.Username, token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repository.GetByUsernameAsync(request.Username) ?? throw new Exception("Invalid credentials");

        var valid = PasswordHelper.Verify(request.Password, user.PasswordSalt, user.PasswordHash);
        if (!valid)
        {
            throw new Exception("Invalid credentials");
        }

        var token = JwtHelper.GenerateToken(user, _config);

        return new AuthResponse(user.Id, user.Username, token);
    }
}