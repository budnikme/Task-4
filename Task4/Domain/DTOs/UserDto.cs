namespace Task4.Domain.Entities.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime LastLoginTime { get; set; }
    public DateTime RegistrationTime { get; set; }
    public string Status { get; set; } = string.Empty;
    
}