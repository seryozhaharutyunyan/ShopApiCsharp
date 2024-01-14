using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class ProductTag
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TageId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductTags")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("TageId")]
    [InverseProperty("ProductTags")]
    public virtual Tag Tage { get; set; } = null!;
}
