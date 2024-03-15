using BasketServices.Application.DTOs;
using BasketServices.Application.Entities;

namespace BasketServices.Application.Services.Interface
{
    public interface ICustomerServices
    {
        Task<CustomerBasket> UpdateQuantityAsync(string customerId, int quantity, int productId);
        Task<UpsertCustomerBasketDTOResponse> AddAsync(UpsertCustomerBasketDTO basket);
        Task<CustomerBasket> GetByIdAsync(string id);
        Task<CustomerBasket> DeleteAsync(string customerId);
        Task<List<CustomerBasket>> GetAllAsync();
    }
}
