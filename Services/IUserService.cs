using SanblasBackend.Models;
using SanblasBackend.DTOs;

namespace SanblasBackend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User> CreateUserAccount(UserCreateDTO dto);
        Task<User?> UpdateUser(int id, UserCreateDTO dto);
    }
}
