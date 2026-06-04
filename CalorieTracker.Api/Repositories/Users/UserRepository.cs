using CalorieTracker.Api.Models;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.QuerySingleOrDefaultAsync<User>
        (
            "spGetUserByUsername",
            new { Username = username },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<User> CreateAsync(string username, string passwordHash, string passwordSalt)
    {
        var id = Guid.NewGuid();

        return await _db.QuerySingleAsync<User>(
            "spCreateUser",
            new
            {
                Id = id,
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            },
            commandType: CommandType.StoredProcedure
        );
    }
}