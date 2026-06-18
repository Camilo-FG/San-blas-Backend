using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services;

public class EventoService : IEventoService
{
    private readonly GlobalContex _context;

    public EventoService(GlobalContex context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventoDto>> GetAllAsync()
    {
        var eventos = await _context.Eventos
            .OrderByDescending(e => e.FechaInicio)
            .ToListAsync();

        return eventos.Select(MapToDto);
    }

    public async Task<IEnumerable<EventoDto>> GetPublicosAsync()
    {
        var hoy = DateTime.UtcNow.Date;

        var eventos = await _context.Eventos
            .Where(e => e.Publicado && e.FechaInicio.Date >= hoy)
            .OrderBy(e => e.FechaInicio)
            .ToListAsync();

        return eventos.Select(MapToDto);
    }

    public async Task<EventoDto?> GetByIdAsync(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        return evento is null ? null : MapToDto(evento);
    }

    public async Task<EventoDto> CreateAsync(EventoDto dto)
    {
        var evento = new Evento
        {
            Titulo = dto.Titulo.Trim(),
            Descripcion = dto.Descripcion.Trim(),
            FechaInicio = DateTime.SpecifyKind(dto.FechaInicio, DateTimeKind.Utc),
            FechaFin = dto.FechaFin.HasValue
                ? DateTime.SpecifyKind(dto.FechaFin.Value, DateTimeKind.Utc)
                : null,
            Lugar = dto.Lugar.Trim(),
            Publicado = dto.Publicado
        };

        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();

        return MapToDto(evento);
    }

    public async Task<EventoDto?> UpdateAsync(int id, EventoDto dto)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento is null) return null;

        evento.Titulo = dto.Titulo.Trim();
        evento.Descripcion = dto.Descripcion.Trim();
        evento.FechaInicio = DateTime.SpecifyKind(dto.FechaInicio, DateTimeKind.Utc);
        evento.FechaFin = dto.FechaFin.HasValue
            ? DateTime.SpecifyKind(dto.FechaFin.Value, DateTimeKind.Utc)
            : null;
        evento.Lugar = dto.Lugar.Trim();
        evento.Publicado = dto.Publicado;

        await _context.SaveChangesAsync();
        return MapToDto(evento);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento is null) return false;

        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();
        return true;
    }

    private static EventoDto MapToDto(Evento evento) => new()
    {
        Id = evento.Id,
        Titulo = evento.Titulo,
        Descripcion = evento.Descripcion,
        FechaInicio = evento.FechaInicio,
        FechaFin = evento.FechaFin,
        Lugar = evento.Lugar,
        Publicado = evento.Publicado
    };
}
