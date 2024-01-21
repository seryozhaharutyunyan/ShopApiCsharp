using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index("EdgeId", Name = "IndexEdge")]
[Index("RegionId", Name = "IndexRegion")]
public partial class City
{
    [Key]
    public int CityId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "NVARCHAR (40)")]
    public string Name { get; set; } = null!;

    public int? RegionId { get; set; }

    public int? EdgeId { get; set; }

    [ForeignKey("EdgeId")]
    [InverseProperty("Cities")]
    public virtual Edge? Edge { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("Cities")]
    public virtual Region? Region { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
