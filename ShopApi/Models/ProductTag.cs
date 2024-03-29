﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class ProductTag
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TagId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductTags")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("TagId")]
    [InverseProperty("ProductTags")]
    public virtual Tag Tag { get; set; } = null!;
}
