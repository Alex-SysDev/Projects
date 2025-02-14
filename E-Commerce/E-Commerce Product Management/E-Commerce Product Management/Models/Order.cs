namespace E_Commerce_Product_Management.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        
        //one to many
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
