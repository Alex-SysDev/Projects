using E_Commerce_Product_Management.Data;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }
        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }
      
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            
            return Save();
        }
        public bool UpdateProduct(Product product)
        {
            _context.Update(product);

            return Save();
        }
        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);

            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        
    }
}
