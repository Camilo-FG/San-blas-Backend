using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet("publicos")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublicos()
    {
        var eventos = await _eventoService.GetPublicosAsync();
        return Ok(eventos);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var eventos = await _eventoService.GetAllAsync();
        return Ok(eventos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var evento = await _eventoService.GetByIdAsync(id);
        if (evento is null) return NotFound();
        return Ok(evento);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] EventoDto dto)
    {
        var created = await _eventoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] EventoDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "El ID de la URL no coincide con el ID del cuerpo." });

        var updated = await _eventoService.UpdateAsync(id, dto);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _eventoService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
