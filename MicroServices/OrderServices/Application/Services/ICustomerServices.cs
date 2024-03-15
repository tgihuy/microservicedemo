using OrderServices.Application.Entities;

namespace OrderServices.Application.Services
{
    public interface ICustomerServices
    {
        public Task<List<Customer>> GetByCustomerIdAsync(string customerId);
        public Task<Customer> AddAsync(Customer customer);
    }
}
