namespace SanblasBackend.Models;

public class LandingContent
{
    public int Id { get; set; }
    public string SectionKey { get; set; } = string.Empty;
    public string JsonData { get; set; } = "{}";
    public DateTime UpdatedAt { get; set; }
}
