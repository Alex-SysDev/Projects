using E_Commerce_Product_Management.Data;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Order> GetOrders() 
        {
            return _context.Orders.OrderBy(p => p.Id).ToList();
        }

        public Order GetOrderById(int id) 
        {
            return _context.Orders.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(p => p.Id == id);
        }

        public bool CreateOrder(Order order)
        {
            _context.Add(order);

            return Save();
        }
        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }        
    }
}
