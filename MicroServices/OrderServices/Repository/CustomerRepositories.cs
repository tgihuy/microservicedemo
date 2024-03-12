using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using OrderServices.Application.Database;
using OrderServices.Application.Entities;
using OrderServices.Application.Repositories;

namespace OrderServices.Repository
{
    public class CustomerRepositories : ICustomerRepositories
    {
        private readonly OrderDbContext _context;
        private readonly ILogger<CustomerRepositories> _logger;
        public CustomerRepositories(OrderDbContext context, ILogger<CustomerRepositories> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                customer.Id = Guid.NewGuid().ToString();
                _context.customers.Add(customer);
                await _context.SaveChangesAsync();
                return await Task.FromResult(customer);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return customer;
            }
        }

        public async Task<List<Customer>> GetByIdentityAsync(string identity)
        {
            List<Customer> customer = new List<Customer>();
            try
            {
                if (!string.IsNullOrEmpty(identity))
                {
                    customer = await _context.customers.Where(c => c.IdentityId.Equals(identity)).ToListAsync();
                }
                return customer;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return customer;
            }
        }
    }
}
