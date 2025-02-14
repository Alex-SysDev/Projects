using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProductById(int id);
        bool ProductExists(int id);

        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
