using System.Security.Cryptography;
using System.Text;

namespace CalorieTracker.Api.Helpers;

public static class PasswordHelper
{
    public static string GenerateSalt()
    {
        var bytes = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static string HashPassword(string password, string salt)
    {
        using var sha = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes(password + salt);
        var hash = sha.ComputeHash(combined);
        return Convert.ToBase64String(hash);
    }

    public static bool Verify(string password, string salt, string storedHash)
    {
        var hash = HashPassword(password, salt);
        return hash == storedHash;
    }
}