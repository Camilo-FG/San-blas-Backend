using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.Models.EntitiesUsuarios;

namespace SanblasBackend.Services;

public interface IAuthenticationService
{
    Task<User?> AuthenticateAsync(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly GlobalContex _context;

    public AuthenticationService(GlobalContex context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        // En un entorno real, deberíamos usar hashing para las contraseñas.
        // Por ahora, validaremos contra la base de datos.
        var hashedPassword = HashPassword(password);
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword && u.State);

        return user;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
