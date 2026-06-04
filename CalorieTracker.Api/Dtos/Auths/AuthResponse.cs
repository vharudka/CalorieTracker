namespace CalorieTracker.Api.Dtos.Auths;

public record AuthResponse(
    Guid UserId,
    string Username,
    string Token
);