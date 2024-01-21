using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR (126)")]
        public string UserEmail { get; set; } = null!;

        [Required]
        [Column(TypeName = "TEXT")]
        public string Token { get; set; } = null!;

        [Required]
        [Column(TypeName = "SMALINT")]
        public bool IsActive { get; set; } = true;
    }
}
