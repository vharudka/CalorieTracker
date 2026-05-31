using CalorieTracker.Api.Models;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.QuerySingleOrDefaultAsync<User>
        (
            "spGetUserByEmail",
            new { Email = email },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<User> CreateAsync(string email, string passwordHash, string passwordSalt)
    {
        var id = Guid.NewGuid();

        return await _db.QuerySingleAsync<User>(
            "spCreateUser",
            new
            {
                Id = id,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            },
            commandType: CommandType.StoredProcedure
        );
    }
}