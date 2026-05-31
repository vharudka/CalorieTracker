using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Repositories;

public interface IUserRepository
{
    Task<Guid> CreateUserAsync(string email, string hash, string salt);
    Task<User?> GetUserByEmailAsync(string email);
}