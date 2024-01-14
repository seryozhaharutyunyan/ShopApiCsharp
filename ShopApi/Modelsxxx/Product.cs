using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("CategoryId", Name = "IndexCategory")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "TEXT (40)")]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(10)]
    [MaxLength(500)]
    [Column(TypeName = "TEXT (500)")]
    public string Description { get; set; } = null!;

    [Required]
    public string Pictures { get; set; } = null!;

    [Required]
    public int Quentity { get; set; }

    public int ReviewsCount { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Product")]
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    [InverseProperty("Product")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
