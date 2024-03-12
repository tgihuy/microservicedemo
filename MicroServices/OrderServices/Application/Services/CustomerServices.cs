using OrderServices.Application.Entities;
using OrderServices.Application.Repositories;

namespace OrderServices.Application.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepositories _repository;
        public CustomerServices(ICustomerRepositories repository) { 
            _repository = repository;
        }
        public Task<Customer> AddAsync(Customer customer)
        {
            return _repository.AddAsync(customer);
        }

        public Task<List<Customer>> GetByIdentityAsync(string identity)
        {
            return _repository.GetByIdentityAsync(identity);
        }
    }
}
