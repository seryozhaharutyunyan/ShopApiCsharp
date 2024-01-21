using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Tag
{
    [Key]
    public int TagId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "NVARCHAR (40)")]
    public string Name { get; set; } = null!;

    [InverseProperty("Tag")]
    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
