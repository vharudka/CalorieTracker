namespace CalorieTracker.Api.Dtos.Auths;

public record AuthResponse(
    Guid UserId,
    string Email,
    string Token
);