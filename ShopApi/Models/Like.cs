using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index("ProductId", Name = "IndexProduct")]
[Index("UserId", Name = "IndexUser")]
public partial class Like
{
    [Key]
    public int LikeId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Likes")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Likes")]
    public virtual User User { get; set; } = null!;
}
