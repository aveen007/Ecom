using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Ecommerce_DBContext : DbContext
    {
        public Ecommerce_DBContext()
        {
        }

        public Ecommerce_DBContext(DbContextOptions<Ecommerce_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategorySpecification> CategorySpecification { get; set; }
        public virtual DbSet<CategorySpecificationValue> CategorySpecificationValue { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecification { get; set; }
        public virtual DbSet<ProductSpecificationValue> ProductSpecificationValue { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<ShippingState> ShippingState { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LFO5DLA\\SQLEXPRESS;Database=Ecommerce_DB;Trusted_Connection=True;User Id=sa;Password=123456789;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategorySpecification>(entity =>
            {
                entity.ToTable("Category_Specification");

                entity.HasIndex(e => e.Specification)
                    .HasName("Unique_Category_Specification")
                    .IsUnique();

                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategorySpecificationValue>(entity =>
            {
                entity.ToTable("Category_Specification_Value");

                entity.HasIndex(e => new { e.CategoryId, e.SpecificationId })
                    .HasName("Unique_Category_Specification_Value")
                    .IsUnique();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategorySpecificationValue)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Specification_Value_Category");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.CategorySpecificationValue)
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Specification_Value_Category_Specification");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasIndex(e => new { e.OrderId, e.ProductId })
                    .HasName("Unique_ProductOrder")
                    .IsUnique();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrder_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrder_Product");
            });

            modelBuilder.Entity<ProductSpecification>(entity =>
            {
                entity.ToTable("Product_Specification");

                entity.HasIndex(e => e.Specification)
                    .HasName("Unique_Product_Specification")
                    .IsUnique();

                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSpecificationValue>(entity =>
            {
                entity.ToTable("Product_Specification_Value");

                entity.HasIndex(e => new { e.ProductId, e.SpecificationId })
                    .HasName("Unique_Product_Specification_Value")
                    .IsUnique();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSpecificationValue)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Specification_Value_Product");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.ProductSpecificationValue)
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Specification_Value_Product_Specification");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Shipping)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipping_Order");

                entity.HasOne(d => d.ShippingState)
                    .WithMany(p => p.Shipping)
                    .HasForeignKey(d => d.ShippingStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipping_ShippingState");
            });

            modelBuilder.Entity<ShippingState>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
