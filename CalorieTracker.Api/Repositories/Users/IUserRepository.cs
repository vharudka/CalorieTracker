using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Repositories.Users;

public interface IUserRepository
{
    Task<User> CreateAsync(string email, string passwordHash, string passwordSalt);
    Task<User?> GetByEmailAsync(string email);
}