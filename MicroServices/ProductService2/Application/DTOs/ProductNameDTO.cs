using System.ComponentModel.DataAnnotations;

namespace ProductService2.Application.DTOs
{
    public class ProductNameDTO
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Name must be longer than 3 char")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
