namespace SanblasBackend.Services;

public class LocalFileStorageService : IFileStorageService
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".jpg", ".jpeg", ".png", ".webp"
    };

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    private readonly string _uploadRoot;

    public LocalFileStorageService(IWebHostEnvironment environment)
    {
        _uploadRoot = Path.Combine(environment.ContentRootPath, "uploads");
        Directory.CreateDirectory(_uploadRoot);
    }

    public async Task<string> SaveCatequesisFileAsync(IFormFile file, string category)
    {
        if (file.Length <= 0)
            throw new InvalidOperationException("El archivo está vacío.");

        if (file.Length > MaxFileSizeBytes)
            throw new InvalidOperationException("El archivo no puede superar 5 MB.");

        var extension = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(extension) || !AllowedExtensions.Contains(extension))
            throw new InvalidOperationException("Formato no permitido. Use PDF, JPG, PNG o WEBP.");

        var safeCategory = string.IsNullOrWhiteSpace(category) ? "general" : category.Trim().ToLowerInvariant();
        var targetDir = Path.Combine(_uploadRoot, "catequesis", safeCategory);
        Directory.CreateDirectory(targetDir);

        var storedName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        var absolutePath = Path.Combine(targetDir, storedName);

        await using var stream = new FileStream(absolutePath, FileMode.CreateNew);
        await file.CopyToAsync(stream);

        return $"catequesis/{safeCategory}/{storedName}";
    }

    public string? GetAbsolutePath(string storedPath)
    {
        if (string.IsNullOrWhiteSpace(storedPath))
            return null;

        var normalized = storedPath.Replace('\\', '/').TrimStart('/');
        var fullPath = Path.GetFullPath(Path.Combine(_uploadRoot, normalized.Replace('/', Path.DirectorySeparatorChar)));

        if (!fullPath.StartsWith(_uploadRoot, StringComparison.OrdinalIgnoreCase))
            return null;

        return File.Exists(fullPath) ? fullPath : null;
    }
}
