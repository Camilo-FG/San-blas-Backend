using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public interface ILandingContentService
{
    Task<IReadOnlyList<LandingSectionDto>> GetAllSectionsAsync();
    Task<LandingSectionDto?> GetSectionAsync(string sectionKey);
    Task<LandingSectionDto?> UpdateSectionAsync(string sectionKey, object data);
}
