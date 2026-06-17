using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;
using System.Security.Cryptography;
using System.Text;

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
                UserRole = u.UserRole,
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
                UserRole = user.UserRole,
                State = user.State,
                CreationDate = user.CreationDate
            };
        }

        public async Task<UserResponseDto> CreateUser(UserCreateDto dto, User? currentUser)
        {
            //validacion de email único
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("El email ya está registrado.");

            //validacion de username único
            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
                throw new Exception("El nombre de usuario ya está en uso.");

            //validacion de contraseñas que coincidan
            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Las contraseñas no coinciden.");

            //validacion de contraseña de un mínimo de 8 caracteres <- ya esta en el dto pero solo por si acaso
            if (dto.Password.Length < 8)
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");

            bool roleToSave = false; //usuario sin permisos de admin por defecto

            //si hay usuario logueado con el rol admin entonces usa el rol del dto
            if (currentUser != null && currentUser.UserRole == true)
            {
                roleToSave = dto.UserRole ?? false; 
            }

            var newUser = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = HashPassword(dto.Password), 
                UserRole = roleToSave,
                State = true,
                CreationDate = DateTime.Now
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                UserRole = newUser.UserRole,
                State = newUser.State,
                CreationDate = newUser.CreationDate
            };
        }

        public async Task<UserResponseDto?> UpdateUser(int id, UserUpdateDto dto, User currentUser)
        {
          
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            if (currentUser.UserRole != true && currentUser.Id != id)
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

                user.Password = HashPassword(dto.Password);
            }

            if (!string.IsNullOrEmpty(dto.UserName))
                user.UserName = dto.UserName;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;

            //solo el admin puede cambiar rol y estado
            if (currentUser.UserRole == true)
            {
                if (dto.UserRole.HasValue)
                    user.UserRole = dto.UserRole.Value;

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
                UserRole = user.UserRole,
                State = user.State,
                CreationDate = user.CreationDate
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}