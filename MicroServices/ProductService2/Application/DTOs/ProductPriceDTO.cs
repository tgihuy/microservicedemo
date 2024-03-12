using System.ComponentModel.DataAnnotations;

namespace ProductService2.Application.DTOs
{
    public class ProductPriceDTO
    {
        public int Id { get; set; }
        [Range(1000.00, 100000.00, ErrorMessage = "Price must be between 1000.00 and 100000.00")]
        public decimal Price { get; set; }
    }
}
