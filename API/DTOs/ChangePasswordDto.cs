using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$", ErrorMessage = "Password does not meet the required complexity!")]
        public string NewPassword { get; set; }
    }
}