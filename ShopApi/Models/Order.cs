using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index("ProductId", Name = "IndexOrderProducts")]
[Index("UserId", Name = "IndexOrderUser")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [Column(TypeName = "DECIMAL")]
    public decimal Price { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int Quentity { get; set; }

    [Column(TypeName = "DATATIME")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "SMALINT")]
    public int Status { get; set; }

    [Required]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Orders")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
