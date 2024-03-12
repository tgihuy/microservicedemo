using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService2.Application.DTOs;
using ProductService2.Application.Entities;
using ProductService2.Application.Services.Interface;

namespace ProductService2.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("productItem/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var item = await _productService.GetByIdAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var item = await _productService.AddAsync(product);
            return Ok(item);
        }

        // PUT: api/Products/5
        [HttpPut("ProductName/{id}")]
        public async Task<ActionResult<Product>> PutProductName(int id, ProductNameDTO product)
        {
            await _productService.UpdateProductName(id, product.Name);
            return Ok("Update thanh cong");
        }

        [HttpPut("ProductPrice/{id}")]
        public async Task<ActionResult<Product>> PutProductPrice(int id, ProductPriceDTO product)
        {
            var item = await _productService.UpdateProductPrice(id, product.Price);
            return Ok(item);
        }

        [HttpPut("ProductQuantity/{id}")]
        public async Task<ActionResult<Product>> PutProductQuantity(int id, ProductAvailableQuantityDTO product)
        {
            await _productService.UpdateProductQuantity(id,product.Quantity);
            return Ok("Update thanh cong");
        }

        [HttpPut("id")]
        public async Task<ActionResult<Product>> PutProduct(int id, ProductDTO product)
        {
            var item = new Product() { Id = product.Id, Name = product.Name, AvailableQuantity = product.Quantity, Price = product.Price };
            return await _productService.UpdateAsync(id, item);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("Delete thanh cong");
        }
    }
}
