namespace SanblasBackend.Services;

public interface IFileStorageService
{
    Task<string> SaveCatequesisFileAsync(IFormFile file, string category);
    string? GetAbsolutePath(string storedPath);
}
