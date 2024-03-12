﻿using System.ComponentModel.DataAnnotations;

namespace ProductService2.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Name must be longer than 3 char")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Range(0, 10000, ErrorMessage = "AvailableQuantity must be between 0 and 10000")]
        public int Quantity { get; set; }
        [Range(1000.00, 100000.00, ErrorMessage = "Price must be between 1000.00 and 100000.00")]
        public decimal Price { get; set; }
    }
}
