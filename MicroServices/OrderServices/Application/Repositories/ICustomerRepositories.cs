using OrderServices.Application.Entities;

namespace OrderServices.Application.Repositories
{
    public interface ICustomerRepositories
    {
        public Task<List<Customer>> GetByCustomerIdAsync(string customerId);
        public Task<Customer> AddAsync(Customer customer);
    }
}
