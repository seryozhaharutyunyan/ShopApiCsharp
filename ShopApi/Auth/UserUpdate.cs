using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Models.Interfaces;

namespace Auth
{
    public class UserUpdate : IModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        public int CityId { get; set; }

        [Column(TypeName = "NVARCHAR (126)")]
        public string Address { get; set; } = null!;

        [Column(TypeName = "NVARCHAR (15)")]
        public string PostCod { get; set; } = null!;
    }
}
