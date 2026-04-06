using CalorieTracker.Api.Models;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Guid> CreateUser(string email, string hash, string salt)
        {
            var sql = "SELECT sp_user_create(@email, @hash, @salt)";
            return await _db.ExecuteScalarAsync<Guid>(sql, new { email, hash, salt });
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var sql = "SELECT * FROM sp_user_get_by_email(@email)";
            return await _db.QueryFirstOrDefaultAsync<User>(sql, new { email });
        }
    }
}
