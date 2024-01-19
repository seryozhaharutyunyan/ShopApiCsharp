using Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index("Email", IsUnique = true)]
[Index("Phone", IsUnique = true)]
[Index("CityId", Name = "IndexCity")]
[Index("RoleId", Name = "IndexRole")]
public partial class User : IModel
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    [Column(TypeName = "NVARCHAR (40)")]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Column(TypeName = "NVARCHAR (126)")]
    public string Email { get; set; } = null!;
    
    [Required]
    [JsonIgnore]
    [MinLength(8)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\\S")]
    [Column(TypeName = "NVARCHAR (126)")]
    public string Password { get; set; } = null!;

    [Required]
    [Phone]
    [Column(TypeName = "NVARCHAR (40)")]
    public string Phone { get; set; } = null!;

    [Required]
    [Column(TypeName = "NVARCHAR (10)")]
    public string Gender { get; set; } = null!;

    [Required]
    [Column(TypeName = "DATEONLY")]
    public DateOnly Age { get; set; }

    public string? Picture { get; set; }

    public int RoleId { get; set; }

    [Required]
    public int CityId { get; set; }

    [Column(TypeName = "NVARCHAR (126)")]
    public string Address { get; set; } = null!;

    [Column(TypeName = "DATETIME")]
    public DateTime CreateAt { get; set; }

    [Column(TypeName = "NVARCHAR (15)")]
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
