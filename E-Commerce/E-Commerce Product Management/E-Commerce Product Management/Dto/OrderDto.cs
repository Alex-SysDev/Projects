using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
