using OrderServices.Application.Entities;

namespace OrderServices.Application.Services
{
    public interface ICustomerServices
    {
        public Task<List<Customer>> GetByIdentityAsync(string identity);
        public Task<Customer> AddAsync(Customer customer);
    }
}
