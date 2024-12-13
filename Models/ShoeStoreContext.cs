using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoeStore.Models;

public partial class ShoeStoreContext : DbContext
{
    public ShoeStoreContext()
    {
    }

    public ShoeStoreContext(DbContextOptions<ShoeStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImageDetail> ProductImageDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-1IM7O79F;Initial Catalog=ShoeStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797AE66B84C");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__ProductID__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserID__37A5467C");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B045EA818");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F65B7A7AE9").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF443A0514");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__3F466844");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C73233BDE");

            entity.ToTable(tb => tb.HasTrigger("trg_CheckStockBeforeInsert"));

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__440B1D61");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__44FF419A");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDEF787943");

            entity.HasIndex(e => e.CategoryId, "IDX_Product_CategoryID");

            entity.HasIndex(e => e.Name, "IDX_Product_Name");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Color).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock).HasDefaultValue(0);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Products__Catego__32E0915F");
        });

        modelBuilder.Entity<ProductImageDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductImageDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductImageDetail_Products");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE68AF471");

            entity.HasIndex(e => e.Email, "UQ_User_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E415A8C856").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534B986EE71").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("customer");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
