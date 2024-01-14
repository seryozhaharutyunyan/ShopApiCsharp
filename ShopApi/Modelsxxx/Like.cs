using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("ProductId", Name = "IndexProduct")]
[Index("UserId", Name = "IndexUser")]
public partial class Like
{
    [Key]
    public int LikeId { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Likes")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Likes")]
    public virtual User User { get; set; } = null!;
}
