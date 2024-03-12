using Microsoft.EntityFrameworkCore;
using ProductService2.Application.Database;
using ProductService2.Application.Entities;
using ProductService2.Application.Repositories.Interface;
using ProductService2.Application.Services.Interface;

namespace ProductService2.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositories _repositories;
        public ProductService(IProductRepositories repositories)
        {
            _repositories = repositories;
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _repositories.AddAsync(product);
        }

        public async Task<Product> DeleteAsync(int id)
        {
            return await _repositories.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _repositories.GetAllProducts();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repositories.GetByIdAsync(id);
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            return await _repositories.UpdateAsync(id, product);
        }

        public async Task<Product> UpdateProductName(int id, string name)
        {
            return await _repositories.UpdateProductName(id, name);
        }

        public async Task<Product> UpdateProductPrice(int id, decimal price)
        {
            return await _repositories.UpdateProductPrice(id, price);
        }

        public async Task<Product> UpdateProductQuantity(int id, int quantity)
        {
            return await _repositories.UpdateProductQuantity(id,quantity);
        }
    }
}
