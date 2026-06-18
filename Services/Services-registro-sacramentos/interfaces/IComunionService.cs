using SanblasBackend.DTOs.DtosRegistroSacramentos;

namespace SanblasBackend.Services
{
    public interface IComunionService
    {
        Task<IEnumerable<ComunionDto>> GetAllAsync();
        Task<ComunionDto?> GetByIdAsync(int id);
        Task<ComunionDto> CreateAsync(ComunionDto dto);
        Task<ComunionDto?> UpdateAsync(int id, ComunionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}