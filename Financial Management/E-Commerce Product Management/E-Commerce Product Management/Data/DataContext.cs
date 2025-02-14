    using E_Commerce_Product_Management.Models;
    using Microsoft.EntityFrameworkCore;

    namespace E_Commerce_Product_Management.Data
    {
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) 
            { 
        
            }

            public DbSet<Category> Categories { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductCategory> ProductCategories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder) 
            {
                // Many-to-Many Relationship: Product <-> Category
                modelBuilder.Entity<ProductCategory>()
                    .HasKey(pc => new { pc.ProductId, pc.CategoryId });

                modelBuilder.Entity<ProductCategory>()
                    .HasOne(p => p.Product)
                    .WithMany(pc => pc.ProductCategories)
                    .HasForeignKey(p => p.ProductId);

                modelBuilder.Entity<ProductCategory>()
                    .HasOne(pc => pc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(pc => pc.CategoryId);

                // One-to-Many Relationship: Order -> OrderItem
                modelBuilder.Entity<OrderItem>()
                    .HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId);

                // One-to-Many Relationship: Product -> OrderItem
                modelBuilder.Entity<OrderItem>()
                    .HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId);

                modelBuilder.Entity<OrderItem>()
                    .Property(oi => oi.UnitPrice)
                    .HasColumnType("decimal(18, 2)");

                modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18, 2)");
            }
        }
    }
