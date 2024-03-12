using System.ComponentModel.DataAnnotations;

namespace ProductService2.Application.DTOs
{
    public class ProductAvailableQuantityDTO
    {
        public int ProductId { get; set; }
        [Range(0, 10000, ErrorMessage = "AvailableQuantity must be between 0 and 10000")]
        public int Quantity { get; set; }
    }
}
