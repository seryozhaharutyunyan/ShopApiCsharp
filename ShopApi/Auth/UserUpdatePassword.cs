using System.ComponentModel.DataAnnotations;

namespace Auth
{
    public class UserUpdatePassword
    {
        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\\S")]
        public string NewPassword { get; set; } = null!;

        [Compare("NewPassword")]
        [MinLength(8)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
