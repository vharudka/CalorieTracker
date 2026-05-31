namespace CalorieTracker.Api.Dtos.Auths;

public record LoginRequest
(
    string Email,
    string Password
);