using OrderServices.Application.Entities;
using OrderServices.DTOs;

namespace OrderServices.Application.Services
{
    public interface IOrderService
    {
        Task<Order> GetByCustomerIdAsync(string customerId);
        Task<UpsertOrderResponseDTO> AddAsync(UpsertOrderDTO upsertOrderDTO);
        Task<Order> DeleteAsync(string orderId);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string orderId);
    }
}
