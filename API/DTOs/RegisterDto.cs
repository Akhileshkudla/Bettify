using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$", ErrorMessage = "Password does not meet the required complexity!")]
    public string Password { get; set; }

    [Required]
    public string DisplayName { get; set; }
    
    [Required]
    public string Username { get; set; }
}