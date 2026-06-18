using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public interface IEventoService
{
    Task<IEnumerable<EventoDto>> GetAllAsync();
    Task<IEnumerable<EventoDto>> GetPublicosAsync();
    Task<EventoDto?> GetByIdAsync(int id);
    Task<EventoDto> CreateAsync(EventoDto dto);
    Task<EventoDto?> UpdateAsync(int id, EventoDto dto);
    Task<bool> DeleteAsync(int id);
}
