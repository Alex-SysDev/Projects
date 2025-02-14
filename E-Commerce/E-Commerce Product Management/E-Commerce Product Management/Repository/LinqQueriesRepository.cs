using E_Commerce_Product_Management.Data;
using E_Commerce_Product_Management.Dto;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Repository
{
    public class LinqQueriesRepository : ILinqQueries
    {
        private readonly DataContext _context;

        public LinqQueriesRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Product> GetProductsBasedOnCategory(int id)
        {   
            var products = _context.ProductCategories
                        .Where(p => p.CategoryId == id)
                        .Select(pc => pc.Product)
                        .ToList();
            return products;
        }
        public ICollection<Order> GetOrderWithinLastMonth()
        {
            var orders = _context.Orders
                        .Where(o => o.OrderDate >= DateTime.Now.AddMonths(-1))
                        .ToList();

            return orders;
        }
        public ICollection<ProductSalesDto> GetTotalSalesOfProduct(int id)
        {
            var totalSales = _context.OrderItems
                .Where(oi => oi.ProductId == id)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(oi => oi.UnitPrice * oi.Quantity)
                })
                .ToList();

            return totalSales;
        }
        public ICollection<ProductSalesDto> GetTopFiveProducts()
        {
            var topFiveProducts = _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(oi => oi.UnitPrice * oi.Quantity)
                })
                .OrderByDescending(p => p.TotalSales)
                .Take(5)
                .ToList();

            return topFiveProducts;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
