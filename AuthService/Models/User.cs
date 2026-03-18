public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; } 
    public required string FullName { get; set; }
    public required string Email { get; set; }
}