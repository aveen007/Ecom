using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_Context.Models
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

        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProduct { get; set; }
        public virtual DbSet<CategoryValue> CategoryValue { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttribute { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductValue> ProductValue { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<ShippingState> ShippingState { get; set; }
        public virtual DbSet<Value> Value { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LFO5DLA\\SQLEXPRESS;Database=Ecommerce_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.ToTable("Category_Product");

                entity.HasIndex(e => new { e.CategoryId, e.ProductId })
                    .HasName("Unique_Category_Product")
                    .IsUnique();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryProduct)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Product_Category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Product_Attribute");
            });

            modelBuilder.Entity<CategoryValue>(entity =>
            {
                entity.ToTable("Category_Value");

                entity.HasIndex(e => new { e.CategoryId, e.ValueId })
                    .HasName("Unique_Category_Value")
                    .IsUnique();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryValue)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Value_Category");

                entity.HasOne(d => d.Value)
                    .WithMany(p => p.CategoryValue)
                    .HasForeignKey(d => d.ValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Value_Value");
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

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("Product_Attribute");

                entity.HasIndex(e => new { e.AttributeId, e.ProductId })
                    .HasName("Unique_Product_Attribute")
                    .IsUnique();

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.ProductAttribute)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Attribute_Attribute");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAttribute)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Attribute_Product");
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

            modelBuilder.Entity<ProductValue>(entity =>
            {
                entity.ToTable("Product_Value");

                entity.HasIndex(e => new { e.ProductId, e.ValueId })
                    .HasName("Unique_Product_Value")
                    .IsUnique();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductValue)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Value_Product");

                entity.HasOne(d => d.Value)
                    .WithMany(p => p.ProductValue)
                    .HasForeignKey(d => d.ValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Value_Value");
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

            modelBuilder.Entity<Value>(entity =>
            {
                entity.Property(e => e.Value1)
                    .IsRequired()
                    .HasColumnName("Value")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
