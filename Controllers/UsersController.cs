using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Models;
using SanblasBackend.Models.EntitiesUsuarios;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _userService.GetAllUsers();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            try
            {
                User? currentUser = null;
                var userIdClaim = User.FindFirst("id")?.Value;

                if (userIdClaim != null)
                {
                    var userDto = await _userService.GetUserById(int.Parse(userIdClaim));
                    if (userDto != null)
                    {
                        currentUser = new User
                        {
                            Id = userDto.Id,
                            UserName = userDto.UserName,
                            Email = userDto.Email,
                            PhoneNumber = userDto.PhoneNumber,
                            UserRole = userDto.UserRole,
                            State = userDto.State,
                            CreationDate = userDto.CreationDate
                        };
                    }
                }

                //crear usuario (pasa currentUser para validar roles)
                var result = await _userService.CreateUser(dto, currentUser);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            try
            {
                //obtener usuario logueado
                var userIdClaim = User.FindFirst("id")?.Value;
                if (userIdClaim == null)
                    return Unauthorized(new { message = "No estás autenticado." });

                var userDto = await _userService.GetUserById(int.Parse(userIdClaim));
                if (userDto == null)
                    return Unauthorized(new { message = "Usuario no encontrado." });

                var currentUser = new User
                {
                    Id = userDto.Id,
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    UserRole = userDto.UserRole,
                    State = userDto.State,
                    CreationDate = userDto.CreationDate
                };

                var result = await _userService.UpdateUser(id, dto, currentUser);
                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}