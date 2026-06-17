using SanblasBackend.DTOs.DtosRegistroSacramentos;

namespace SanblasBackend.Services
{
    public interface IMatrimonioService
    {
        Task<IEnumerable<MatrimonioDto>> GetAllAsync();
        Task<MatrimonioDto?> GetByIdAsync(int id);
        Task<MatrimonioDto> CreateAsync(MatrimonioDto dto);
        Task<MatrimonioDto?> UpdateAsync(int id, MatrimonioDto dto);
        Task<bool> DeleteAsync(int id);
    }
}