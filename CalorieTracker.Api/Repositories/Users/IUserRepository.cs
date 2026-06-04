using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Repositories.Users;

public interface IUserRepository
{
    Task<User> CreateAsync(string username, string passwordHash, string passwordSalt);
    Task<User?> GetByUsernameAsync(string username);
}