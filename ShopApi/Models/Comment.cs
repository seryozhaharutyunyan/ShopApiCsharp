using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index("ProductId", Name = "IndexProductUser")]
[Index("UserId", Name = "IndexUserComment")]
public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(255)]
    [Column(TypeName = "TEXT (255)")]
    public string Content { get; set; } = null!;

    [Column(TypeName = "SMALINT")]
    public int Published { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Comments")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}
