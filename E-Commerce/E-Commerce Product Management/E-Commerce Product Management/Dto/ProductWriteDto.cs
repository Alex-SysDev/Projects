namespace E_Commerce_Product_Management.Dto
{
    public class ProductWriteDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
