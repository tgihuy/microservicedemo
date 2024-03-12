using Microsoft.EntityFrameworkCore;
using ProductService2.Application.Database;
using ProductService2.Application.Entities;
using ProductService2.Application.Repositories.Interface;

namespace ProductService2.Application.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductRepositories> _logger;
        public ProductRepositories(ProductContext context, ILogger<ProductRepositories> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product> AddAsync(Product product)
        {
            try
            {
                if (product == null)
                {
                    return null;
                }
                _context.products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Product> DeleteAsync(int id)
        {
            try
            {
                var deleteitem = await _context.products.FindAsync(id);
                if (deleteitem != null)
                {
                    _context.products.Remove(deleteitem);
                    await _context.SaveChangesAsync();
                    return await Task.FromResult<Product>(deleteitem);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return await _context.products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var item = await _context.products.FindAsync(id);
                if (item == null)
                {
                    return null;
                }
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            try
            {
                var updateitem = await _context.products.FindAsync(id);
                if (updateitem != null)
                {
                    updateitem.Id = product.Id;
                    updateitem.Name = product.Name;
                    updateitem.Price = product.Price;
                    updateitem.AvailableQuantity = product.AvailableQuantity;

                    _context.Entry(updateitem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return updateitem;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Product> UpdateProductName(int id, string name)
        {
            try
            {
                var updateproduct = await _context.products.FindAsync(id);
                if (updateproduct != null)
                {
                    updateproduct.Name = name;

                    _context.Entry(updateproduct).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return updateproduct;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Product> UpdateProductPrice(int id, decimal price)
        {
            try
            {
                var updateproduct = await _context.products.FindAsync(id);
                if (updateproduct != null )
                {
                    updateproduct.Price = price;

                    _context.Entry(updateproduct).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return updateproduct;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Product> UpdateProductQuantity(int id, int quantity)
        {
            try
            {
                var updateproduct = await _context.products.FindAsync(id);
                if (updateproduct != null)
                {
                    updateproduct.AvailableQuantity = quantity;

                    _context.Entry(updateproduct).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return updateproduct;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
