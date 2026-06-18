using System.Security.Cryptography;
using System.Text;

namespace SanblasBackend.Utils;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public static bool LooksHashed(string password) =>
        password.Length == 44 && password.EndsWith('=');
}
