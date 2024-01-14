using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("Email", IsUnique = true)]
[Index("Phone", IsUnique = true)]
[Index("CityId", Name = "IndexCity")]
[Index("RoleId", Name = "IndexRole")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "TEXT (40)")]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Column(TypeName = "TEXT (126)")]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    [Column(TypeName = "TEXT (40)")]
    public string Phone { get; set; } = null!;

    [Required]
    [Column(TypeName = "TEXT (10)")]
    public string Gender { get; set; } = null!;

    [Required]
    [Column(TypeName = "TEXT")]
    public DateOnly Age { get; set; }

    public string? Picture { get; set; }

    public int RoleId { get; set; }

    [Required]
    public int CityId { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(126)]
    [Column(TypeName = "TEXT (126)")]
    public string Address { get; set; } = null!;

    [Column(TypeName = "TEXT")]
    public DateOnly CreateAt { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(15)]
    [Column(TypeName = "TEXT (15)")]
    public string PostCod { get; set; } = null!;

    [ForeignKey("CityId")]
    [InverseProperty("Users")]
    public virtual City City { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
