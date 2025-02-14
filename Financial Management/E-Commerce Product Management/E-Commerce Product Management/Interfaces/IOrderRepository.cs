using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrderById(int id);
        bool OrderExists(int id);

        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
