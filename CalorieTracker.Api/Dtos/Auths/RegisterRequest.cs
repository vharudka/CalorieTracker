namespace CalorieTracker.Api.Dtos.Auths;

public record RegisterRequest
(
    string Email,
    string Password
);