using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Exceptions;
using CalorieTracker.Api.Helpers;
using CalorieTracker.Api.Repositories.UserGoals;
using CalorieTracker.Api.Repositories.Users;

namespace CalorieTracker.Api.Services.Auths;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IUserGoalsRepository _userGoalsRepository;
    private readonly IConfiguration _config;

    public AuthService
    (
        IUserRepository repository,
        IUserGoalsRepository userGoalsRepository,
        IConfiguration config
    )
    {
        _repository = repository;
        _userGoalsRepository = userGoalsRepository;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _repository.GetByUsernameAsync(request.Username);
        if (existing is not null)
        {
            throw new UsernameAlreadyExistsException(request.Username);
        }

        var salt = PasswordHelper.GenerateSalt();
        var hash = PasswordHelper.HashPassword(request.Password, salt);

        var user = await _repository.CreateAsync(request.Username, hash, salt);
        var token = JwtHelper.GenerateToken(user, _config);

        await _userGoalsRepository.UpsertAsync(user.Id, new SetUserGoalsRequest(2000));

        return new AuthResponse(user.Id, user.Username, token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repository.GetByUsernameAsync(request.Username) ?? throw new InvalidCredentialsException();

        var valid = PasswordHelper.Verify(request.Password, user.PasswordSalt, user.PasswordHash);
        if (!valid)
        {
            throw new InvalidCredentialsException();
        }

        var token = JwtHelper.GenerateToken(user, _config);

        return new AuthResponse(user.Id, user.Username, token);
    }
}