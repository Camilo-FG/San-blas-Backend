using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models.EntitiesUsuarios;
using SanblasBackend.Utils;

namespace SanblasBackend.Services
{
    public class UserService : IUserService
    {
        private readonly GlobalContex _context;

        public UserService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                State = u.State,
                CreationDate = u.CreationDate
            });
        }

        public async Task<UserResponseDto?> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                State = user.State,
                CreationDate = user.CreationDate
            };
        }

        public async Task<UserResponseDto> CreateUser(UserCreateDto dto, User? currentUser)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("El email ya está registrado.");

            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
                throw new Exception("El nombre de usuario ya está en uso.");

            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Las contraseñas no coinciden.");

            if (dto.Password.Length < 8)
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");

            var roleToSave = UserRoles.User;

            if (currentUser != null && UserRoles.IsAdmin(currentUser.Role))
            {
                roleToSave = UserRoles.Normalize(dto.Role);
            }

            var newUser = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = PasswordHasher.Hash(dto.Password),
                Role = roleToSave,
                State = true,
                CreationDate = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Role = newUser.Role,
                State = newUser.State,
                CreationDate = newUser.CreationDate
            };
        }

        public async Task<UserResponseDto?> UpdateUser(int id, UserUpdateDto dto, User currentUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            if (!UserRoles.IsAdmin(currentUser.Role) && currentUser.Id != id)
                throw new Exception("No tienes permiso para actualizar este usuario.");

            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id))
                    throw new Exception("El email ya está registrado por otro usuario.");
            }

            if (!string.IsNullOrEmpty(dto.UserName) && dto.UserName != user.UserName)
            {
                if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName && u.Id != id))
                    throw new Exception("El nombre de usuario ya está en uso.");
            }

            if (!string.IsNullOrEmpty(dto.Password))
            {
                if (dto.Password != dto.ConfirmPassword)
                    throw new Exception("Las contraseñas no coinciden.");

                if (dto.Password.Length < 8)
                    throw new Exception("La contraseña debe tener al menos 8 caracteres.");

                user.Password = PasswordHasher.Hash(dto.Password);
            }

            if (!string.IsNullOrEmpty(dto.UserName))
                user.UserName = dto.UserName;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;

            if (UserRoles.IsAdmin(currentUser.Role))
            {
                if (!string.IsNullOrWhiteSpace(dto.Role))
                    user.Role = UserRoles.Normalize(dto.Role);

                if (dto.State.HasValue)
                    user.State = dto.State.Value;
            }

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                State = user.State,
                CreationDate = user.CreationDate
            };
        }
    }
}
