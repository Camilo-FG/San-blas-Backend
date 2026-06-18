using SanblasBackend.Utils;

namespace SanblasBackend.Models.EntitiesUsuarios;

public class User
{
	public int Id { get; set; }
	public string UserName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Role { get; set; } = UserRoles.User;
	public bool State { get; set; }
	public DateTime CreationDate { get; set; }
}
