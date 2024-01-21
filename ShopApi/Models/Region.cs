using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index("EdgesId", Name = "IndexEdges")]
public partial class Region
{
    [Key]
    public int RegionId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "NVARCHAR (40)")]
    public string Name { get; set; } = null!;

    [Required]
    public int EdgesId { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [ForeignKey("EdgesId")]
    [InverseProperty("Regions")]
    public virtual Edge Edges { get; set; } = null!;
}
