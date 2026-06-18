using SanblasBackend.DTOs.DtosRegistroSacramentos;

namespace SanblasBackend.Services
{
    public interface IBautismoService
    {
        Task<IEnumerable<BautismoDto>> GetAllAsync();
        Task<BautismoDto?> GetByIdAsync(int id);
        Task<BautismoDto> CreateAsync(BautismoDto dto);
        Task<BautismoDto?> UpdateAsync(int id, BautismoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}