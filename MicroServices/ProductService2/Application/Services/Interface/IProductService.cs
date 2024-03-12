using ProductService2.Application.Entities;

namespace ProductService2.Application.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> UpdateProductName(int id, string name);
        Task<Product> UpdateProductPrice(int id, decimal price);
        Task<Product> UpdateProductQuantity(int id, int quantity);
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(int id, Product product);
        Task<Product> DeleteAsync(int id);
    }
}
