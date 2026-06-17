using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UsersController(IUserService UserService) 
        {
            _UserService = UserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _UserService.GetAllUsers();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _UserService.GetUserById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromBody] UserCreateDTO dto)
        {
            try
            {
                var result = await _UserService.CreateUserAccount(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    innerInner = ex.InnerException?.InnerException?.Message
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateDTO dto)
        {
            var result = await _UserService.UpdateUser(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
