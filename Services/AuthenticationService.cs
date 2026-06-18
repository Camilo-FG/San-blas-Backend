using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.Models.EntitiesUsuarios;
using SanblasBackend.Utils;

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
        var hashedPassword = PasswordHasher.Hash(password);
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword && u.State);

        return user;
    }
}
