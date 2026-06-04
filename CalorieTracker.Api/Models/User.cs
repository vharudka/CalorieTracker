namespace CalorieTracker.Api.Models;

public record User
(
    Guid Id,
    string Username,
    string PasswordHash,
    string PasswordSalt
);