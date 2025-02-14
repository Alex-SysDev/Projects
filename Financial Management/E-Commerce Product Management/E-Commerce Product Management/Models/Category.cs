namespace E_Commerce_Product_Management.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //many to many
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
