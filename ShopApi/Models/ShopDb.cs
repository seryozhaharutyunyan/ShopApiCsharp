using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class ShopDb : DbContext
{

    public ShopDb()
    {
    }

    public ShopDb(DbContextOptions<ShopDb> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Edge> Edges { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductTag> ProductTags { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
         optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
    }
       

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasOne(d => d.Edge).WithMany(p => p.Cities).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Region).WithMany(p => p.Cities).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Edge>(entity =>
        {
            entity.Property(e => e.EdgeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Products).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.RoleId).HasDefaultValue(2);

            entity.HasOne(d => d.City).WithMany(p => p.Users).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
