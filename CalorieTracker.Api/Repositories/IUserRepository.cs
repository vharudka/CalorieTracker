using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> CreateUser(string email, string hash, string salt);
        Task<User?> GetUserByEmail(string email);
    }
}
