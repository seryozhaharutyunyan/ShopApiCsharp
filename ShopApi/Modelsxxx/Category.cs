using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    [Column(TypeName = "TEXT (40)")]
    public string Name { get; set; } = null!;

    [MinLength(10)]
    [MaxLength(255)]
    [Column(TypeName = "TEXT (255)")]
    public string? Description { get; set; }

    public string? Picture { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
