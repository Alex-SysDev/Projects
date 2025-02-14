using E_Commerce_Product_Management.Data;
using E_Commerce_Product_Management.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_Commerce_Product_Management
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            this._context = context;
        }
        public void SeedDataContext()
        {
            if (!_context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category() { Name = "Electronics", Description = "Gadgets and devices" },
                    new Category() { Name = "Clothing", Description = "Apparel for men and women" },
                    new Category() { Name = "Home & Kitchen", Description = "Appliances and furniture" }
                };
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }

            if (!_context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product() { Name = "Smartphone", Description = "Latest model smartphone", Price = 599.99m, StockQuantity = 100 },
                    new Product() { Name = "Laptop", Description = "High performance laptop", Price = 1299.99m, StockQuantity = 50 },
                    new Product() { Name = "Blender", Description = "Powerful kitchen blender", Price = 49.99m, StockQuantity = 200 }
                };
                _context.Products.AddRange(products);
                _context.SaveChanges();
            }

            if (!_context.ProductCategories.Any())
            {
                var productCategories = new List<ProductCategory>()
                {
                    new ProductCategory() { ProductId = 1, CategoryId = 1 }, // Smartphone -> Electronics
                    new ProductCategory() { ProductId = 2, CategoryId = 1 }, // Laptop -> Electronics
                    new ProductCategory() { ProductId = 3, CategoryId = 3 }, // Blender -> Home & Kitchen
                };
                _context.ProductCategories.AddRange(productCategories);
                _context.SaveChanges();
            }

            if (!_context.Orders.Any())
            {
                var orders = new List<Order>()
                {
                    new Order() { CustomerName = "John Doe", OrderDate = DateTime.Now },
                    new Order() { CustomerName = "Jane Smith", OrderDate = DateTime.Now.AddDays(-1) }
                };
                _context.Orders.AddRange(orders);
                _context.SaveChanges();
            }

            if (!_context.OrderItems.Any())
            {
                var orderItems = new List<OrderItem>()
                {
                    new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 599.99m }, // Order 1 -> Smartphone
                    new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 1299.99m }, // Order 1 -> Laptop
                    new OrderItem() { OrderId = 2, ProductId = 3, Quantity = 2, UnitPrice = 49.99m }  // Order 2 -> Blender (2 items)
                };
                _context.OrderItems.AddRange(orderItems);
                _context.SaveChanges();
            }
        }
    }
}
