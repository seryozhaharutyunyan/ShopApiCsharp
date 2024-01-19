using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserUpdatePassword : IModel
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
