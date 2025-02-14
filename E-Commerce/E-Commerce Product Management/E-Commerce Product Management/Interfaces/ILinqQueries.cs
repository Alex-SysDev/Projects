using E_Commerce_Product_Management.Dto;
using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Interfaces
{
    public interface ILinqQueries
    {
        ICollection<Product> GetProductsBasedOnCategory(int id);
        ICollection<Order> GetOrderWithinLastMonth();
        ICollection<ProductSalesDto> GetTotalSalesOfProduct(int id);
        ICollection<ProductSalesDto> GetTopFiveProducts();
        bool CategoryExists(int id);
        bool ProductExists(int id);
    }
}
