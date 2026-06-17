using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services
{
    public class UserService : IUserService
    {
        private readonly GlobalContex _context;

        public UserService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAccount(UserCreateDTO dto) 
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password, // Recordatorio: En producción usar Hashing
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber,
                State = true,
                CreationDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateUser(int id, UserCreateDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Role = dto.Role;
            user.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return user;
        }
        //falta el update
    }
}
