namespace ProductService2.Application.DTOs
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}