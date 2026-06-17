namespace SanBlasBackend.Models;

public class User { 
	public int Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; }
	public int PhoneNumber { get; set; }
	public string Password { get; set; }
	public string UserRole { get; set; }
	public bool State { get; set; } //esto no deberia de ser IsActive o Status? o UserStatus?
	public DateTime CreationDate { get; set; }
}
