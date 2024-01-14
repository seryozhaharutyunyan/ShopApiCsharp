using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class Edge
{
    [Key]
    public int EdgeId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "NVARCHAR (40)")]
    public string Name { get; set; } = null!;

    [InverseProperty("Edge")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [InverseProperty("Edges")]
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
