namespace E_Commerce_Product_Management.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        //one to many
        public ICollection<OrderItem> OrderItems { get; set; }

        //many to many
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
