namespace CalorieTracker.Api.Dtos.Auths;

public record LoginRequest
(
    string Username,
    string Password
);