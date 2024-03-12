using Microsoft.EntityFrameworkCore;
using OrderServices.Application.Database;
using OrderServices.Application.Entities;
using OrderServices.Application.Repositories;

namespace OrderServices.Repository
{
    public class OrderRepositories : IOrderRepositories
    {
        private readonly OrderDbContext _context;
        private readonly ILogger<OrderRepositories> _logger;
        public OrderRepositories(OrderDbContext context, ILogger<OrderRepositories> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> AddAsync(Order order)
        {
            try 
            { 
                order.Id = Guid.NewGuid().ToString();
                foreach (var item in order.Items)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                _context.orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
}

        public async Task<Order> DeleteAsync(string orderId)
        {
            try
            {
                var order = await GetByIdAsync(orderId);
                if (order != null)
                {
                    _context.orders.Remove(order);
                    await _context.SaveChangesAsync();
                    return await Task.FromResult(new Order());
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

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                var result = await _context.orders.ToListAsync();
                for (int i = 0; i < result.Count; i++)
                {
                    await _context.Entry(result[i]).Collection(i => i.Items).LoadAsync();
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Order> GetByCustomerIdAsync(string customerId)
        {
            try
            {
                return await _context.orders.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            try
            {
                var result = await _context.orders.FirstOrDefaultAsync(o => o.Id == id);
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
    }
}
