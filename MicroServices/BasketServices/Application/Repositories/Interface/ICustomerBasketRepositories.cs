using BasketServices.Application.Entities;

namespace BasketServices.Application.Repositories.Interface
{
    public interface ICustomerBasketRepositories
    {
        Task<CustomerBasket> UpdateQuantityAsync(string customerId, int quantity, int productId);
        Task<CustomerBasket> AddBasketItem(CustomerBasket basket);
        Task<CustomerBasket> GetByIdAsync(string id);
        Task<CustomerBasket> DeleteAsync(string customerId);
        Task<List<CustomerBasket>> GetAllAsync();
        Task<CustomerBasket> AddCustomerBasketItem(CustomerBasket basket);
    }
}
