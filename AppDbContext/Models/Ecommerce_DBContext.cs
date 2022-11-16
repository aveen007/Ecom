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

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryPromotion> CategoryPromotion { get; set; }
        public virtual DbSet<CategorySpecification> CategorySpecification { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategoryValue> ProductCategoryValue { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecification { get; set; }
        public virtual DbSet<ProductSpecificationValue> ProductSpecificationValue { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<ShippingState> ShippingState { get; set; }
        public virtual DbSet<Specification> Specification { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRating> UserRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Ecommerce_DB;Trusted_Connection=True;User Id=sa;Password=123456789;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Governorate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
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

            modelBuilder.Entity<CategoryPromotion>(entity =>
            {
                entity.ToTable("Category_Promotion");

                entity.HasIndex(e => new { e.CategoryId, e.PromotionId })
                    .HasName("Unique_Category_Promotion")
                    .IsUnique();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryPromotion)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Promotion_Category");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.CategoryPromotion)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Promotion_Promotion");
            });

            modelBuilder.Entity<CategorySpecification>(entity =>
            {
                entity.ToTable("Category_Specification");

                entity.HasIndex(e => new { e.CategoryId, e.SpecificationId })
                    .HasName("Unique_Category_Specification_Value")
                    .IsUnique();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategorySpecification)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Specification_Value_Category");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.CategorySpecification)
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Specification_Value_Category_Specification");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Notification_Type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("Notification_Type");

                entity.HasIndex(e => e.Name)
                    .HasName("Unique_Notification_Type")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.ShippingId })
                    .HasName("Unique_Order")
                    .IsUnique();

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ImageLink)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

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

            modelBuilder.Entity<ProductCategoryValue>(entity =>
            {
                entity.ToTable("Product_Category_Value");

                entity.HasIndex(e => new { e.CategorySpecificationId, e.ProductId, e.Value })
                    .HasName("Unique_Product_Category_Value")
                    .IsUnique();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategorySpecification)
                    .WithMany(p => p.ProductCategoryValue)
                    .HasForeignKey(d => d.CategorySpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category_Value_Category_Specification");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategoryValue)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category_Value_Product");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasIndex(e => new { e.OrderId, e.ProductId })
                    .HasName("Unique_ProductOrder")
                    .IsUnique();

                entity.Property(e => e.SinglePrice).HasColumnType("numeric(18, 2)");

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

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.Property(e => e.ShippingPrice).HasColumnType("numeric(18, 2)");

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

            modelBuilder.Entity<Specification>(entity =>
            {
                entity.HasIndex(e => e.Specification1)
                    .HasName("Unique_Category_Specification")
                    .IsUnique();

                entity.Property(e => e.Specification1)
                    .IsRequired()
                    .HasColumnName("Specification")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(400);

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

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasColumnType("numeric(10, 0)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Address");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.ToTable("User_Rating");

                entity.HasIndex(e => new { e.UserId, e.ProductOrderId })
                    .HasName("Unique_User_Rating")
                    .IsUnique();

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasOne(d => d.ProductOrder)
                    .WithMany(p => p.UserRating)
                    .HasForeignKey(d => d.ProductOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Rating_ProductOrder");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRating)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Rating_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
