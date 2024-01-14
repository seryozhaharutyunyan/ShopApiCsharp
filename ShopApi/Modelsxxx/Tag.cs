using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

public partial class Tag
{
    [Key]
    public int TageId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "TEXT (40)")]
    public string Name { get; set; } = null!;

    [InverseProperty("Tage")]
    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
