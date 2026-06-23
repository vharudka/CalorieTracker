namespace CalorieTracker.Api.Dtos.Auths;

public record AuthRequest
(
    string Username,
    string Password
);