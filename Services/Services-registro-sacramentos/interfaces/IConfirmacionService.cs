using SanblasBackend.DTOs.DtosRegistroSacramentos;

namespace SanblasBackend.Services
{
    public interface IConfirmacionService
    {
        Task<IEnumerable<ConfirmacionDto>> GetAllAsync();
        Task<ConfirmacionDto?> GetByIdAsync(int id);
        Task<ConfirmacionDto> CreateAsync(ConfirmacionDto dto);
        Task<ConfirmacionDto?> UpdateAsync(int id, ConfirmacionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}