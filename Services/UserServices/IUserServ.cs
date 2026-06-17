using SanblasBackend.DTOs;
using SanblasBackend.Models;
using SanblasBackend.Models.EntitiesUsuarios;

namespace SanblasBackend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsers();
        Task<UserResponseDto?> GetUserById(int id);
        Task<UserResponseDto> CreateUser(UserCreateDto dto, User? currentUser);
        Task<UserResponseDto?> UpdateUser(int id, UserUpdateDto dto, User currentUser);
    }
}