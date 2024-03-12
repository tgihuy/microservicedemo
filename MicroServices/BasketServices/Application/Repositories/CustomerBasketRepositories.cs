using System.ComponentModel.DataAnnotations;
using BasketServices.Application.Entities;
using BasketServices.Application.Repositories.Interface;
using Microservices.Application.Database;
using Microsoft.EntityFrameworkCore;

namespace BasketServices.Application.Repositories
{
    public class CustomerBasketRepositories : ICustomerBasketRepositories
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<CustomerBasketRepositories> _logger;
        public CustomerBasketRepositories(ProductDbContext context, ILogger<CustomerBasketRepositories> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CustomerBasket> GetByIdAsync(string id)
        {
            try
            {
                var result = await _context.CustomerBaskets.FirstOrDefaultAsync(c => c.CustomerId == id);
                if (result != null)
                {
                    await _context.Entry(result).Collection(i => i.Items).LoadAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
        public async Task<CustomerBasket> AddBasketItem(CustomerBasket basket)
        {
            try
            {
                var customerBasketDb = await GetByIdAsync(basket.CustomerId);
                if (customerBasketDb != null)
                {
                    for(int i=0; i < basket.Items.Count; i++)
                    {
                        basket.Items[i].Id = Guid.NewGuid().ToString();
                    }
                    customerBasketDb.Items.AddRange(basket.Items);
                    _context.CustomerBaskets.Update(customerBasketDb);
                }
                await _context.SaveChangesAsync();
                return customerBasketDb;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<CustomerBasket> DeleteAsync(string customerId)
        {
            try
            {
                var customerBasket = await GetByIdAsync(customerId);
                if (customerBasket != null)
                {
                    _context.BasketItems.RemoveRange(customerBasket.Items);
                    _context.CustomerBaskets.Remove(customerBasket);
                   await _context.SaveChangesAsync();
                }
                return await Task.FromResult(new CustomerBasket());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<CustomerBasket>> GetAllAsync()
        {
            try
            {
                var result = await _context.CustomerBaskets.ToListAsync();
                for (int i = 0; i < result.Count; i++)
                {
                    await _context.Entry(result[i]).Collection(i => i.Items).LoadAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        

        public async Task<CustomerBasket> UpdateQuantityAsync(string customerId, int quantity, int productId)
        {
            try
            {
                var customerBasket = await GetByIdAsync(customerId);
                if (customerBasket != null) {
                    foreach (var item in customerBasket.Items)
                    {
                        if(item.ProductId == productId)
                        {
                            item.Quantity = quantity;
                        }
                    }
                }
                _context.Entry(customerBasket).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return customerBasket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }

        }

        public async Task<CustomerBasket> AddCustomerBasketItem(CustomerBasket basket)
        {
            try
            {        
                foreach (var item in basket.Items)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                _context.CustomerBaskets.Add(basket);
                await _context.SaveChangesAsync();
                return basket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
