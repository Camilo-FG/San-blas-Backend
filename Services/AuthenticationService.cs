using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.Models;

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
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        return user;
    }
}
