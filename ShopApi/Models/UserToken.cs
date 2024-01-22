using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "NVARCHAR (126)")]
        public string UserEmail { get; set; } = null!;

        [Required]
        [Column(TypeName = "TEXT")]
        public string AccessToken { get; set; } = null!;

        [Required]
        [Column(TypeName = "TEXT")]
        public string RefreshToken { get; set; } = null!;

        [Required]
        [Column(TypeName = "SMALINT")]
        public int IsActive { get; set; } = 1;
    }
}
