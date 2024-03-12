using OrderServices.Application.Entities;

namespace OrderServices.Application.Repositories
{
    public interface IOrderRepositories
    {
        Task<Order> GetByCustomerIdAsync(string customerId);
        Task<Order> AddAsync(Order order);
        Task<Order> DeleteAsync(string orderId);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string orderId);
    }
}
