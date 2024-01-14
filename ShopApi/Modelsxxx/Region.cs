using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("EdgesId", Name = "IndexEdges")]
public partial class Region
{
    [Key]
    public int RegionId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "TEXT (40)")]
    public string Name { get; set; } = null!;

    public int EdgesId { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [ForeignKey("EdgesId")]
    [InverseProperty("Regions")]
    public virtual Edge Edges { get; set; } = null!;
}
