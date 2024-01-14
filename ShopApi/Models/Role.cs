﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;


namespace Models
{

    public partial class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(16)]
        [Column(TypeName = "NVARCHAR (16)")]
        public string Name { get; set; } = null!;

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
