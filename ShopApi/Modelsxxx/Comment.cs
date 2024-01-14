using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("ProductId", Name = "IndexProductUser")]
[Index("UserId", Name = "IndexUserComment")]
public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(255)]
    [Column(TypeName = "TEXT (255)")]
    public string Content { get; set; } = null!;

    [Column(TypeName = "INTEGER")]
    public bool Published { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Comments")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}
