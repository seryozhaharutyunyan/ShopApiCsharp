using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; } = null!;

        [Required]
        public string RefreshToken { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
