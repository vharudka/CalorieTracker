namespace CalorieTracker.Api.Models;

public record User
(
    Guid Id,
    string Email,
    string PasswordHash,
    string PasswordSalt
);