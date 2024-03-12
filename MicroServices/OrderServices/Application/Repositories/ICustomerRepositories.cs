using OrderServices.Application.Entities;

namespace OrderServices.Application.Repositories
{
    public interface ICustomerRepositories
    {
        public Task<List<Customer>> GetByIdentityAsync(string identity);
        public Task<Customer> AddAsync(Customer customer);
    }
}
