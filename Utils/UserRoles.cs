namespace SanblasBackend.Utils;

public static class UserRoles
{
    public const string Admin = "admin";
    public const string User = "user";
    public const string JwtAdmin = "Admin";
    public const string JwtUser = "User";

    public static bool IsAdmin(string? role) =>
        role?.Equals(Admin, StringComparison.OrdinalIgnoreCase) == true ||
        role?.Equals(JwtAdmin, StringComparison.OrdinalIgnoreCase) == true;

    public static string Normalize(string? role) => IsAdmin(role) ? Admin : User;

    public static string ToJwtRole(string? role) => IsAdmin(role) ? JwtAdmin : JwtUser;
}
