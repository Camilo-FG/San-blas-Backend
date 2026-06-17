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
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<User> CreateUser( dto)  //debo de agregar el DTO
        {
            var solicitud = new User
            {
                //aqui debo de poner los datos del DTO
            };

            _context.Users.Add(solicitud);
            await _context.SaveChangesAsync();

            return solicitud;
        }
    }
}
