using System;
using System.Collections.Generic;
using LAB8_David_Belizario.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Data;

public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext()
    {
    }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Orderdetail> Orderdetails { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.HasOne(d => d.Client)
                .WithMany(p => p.Orders)
                .HasConstraintName("fk_orders_client");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.Orderdetails)
                .HasConstraintName("fk_orderdetails_order");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Orderdetails)
                .HasConstraintName("fk_orderdetails_product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
