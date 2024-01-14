using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("ProductId", Name = "IndexOrderProducts")]
[Index("UserId", Name = "IndexOrderUser")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [Column(TypeName = "TEXT")]
    public decimal Price { get; set; }

    public int UserId { get; set; }

    [Required]
    public int Quentity { get; set; }
    
    [Column(TypeName = "TEXT")]
    public DateOnly OrderDate { get; set; }

    public int Status { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Orders")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
