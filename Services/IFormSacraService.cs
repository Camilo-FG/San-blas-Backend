using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services
{
    public interface IFormSacraService
    {
        Task<IEnumerable<FormSacra>> GetAllSolicitudes();
        Task<FormSacra?> GetSolicitudById(int id);
        Task<FormSacra> CreateSolicitud(SolicSacraCreateDto dto);
        Task<FormSacra?> UpdateEstado(int id, string nuevoEstado);
    }
}
