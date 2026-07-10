using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services;

public interface IDonacionService
{
    Task<IEnumerable<Donacion>> GetAllDonaciones();
    Task<Donacion?> GetDonacionById(int id);
    Task<Donacion> CreateDonacion(DonacionCreateDto dto);
    Task<(Donacion? Donacion, bool CorreoEnviado)> UpdateEstado(int id, string nuevoEstado);
}