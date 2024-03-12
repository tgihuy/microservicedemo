using System.Text.Json;
using OrderServices.Application.Entities;
using OrderServices.Application.Repositories;
using OrderServices.DTOs;

namespace OrderServices.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepositories _repositories;
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;
        private readonly IConfiguration _configuration;
        public OrderService(IOrderRepositories repositories, HttpClient httpClient, ILogger<OrderService> logger, IConfiguration configuration)
        {
            _repositories = repositories;
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<UpsertOrderResponseDTO> AddAsync(UpsertOrderDTO upsertOrderDTO)
        {
            string apiGetBasketId = _configuration["HttpGetCustomerBasket"] + "/" + upsertOrderDTO.IdentityId;
            Order order = new Order();
            UpsertOrderResponseDTO upsertOrderResponseDTO = new UpsertOrderResponseDTO("", order);

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(apiGetBasketId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var customerBasket = await responseMessage.Content.ReadFromJsonAsync<CustomerBasket>();
                if (customerBasket != null && customerBasket.Items != null && customerBasket.Items.Any())
                {
                    // Kiểm tra xem sản phẩm còn đủ hàng không
                    bool productsAvailable = await CheckProductsAvailability(customerBasket.Items);

                    if (productsAvailable)
                    {
                        order.OrderDate = DateTime.Now;
                        order.Street = upsertOrderDTO.Street;
                        order.City = upsertOrderDTO.City;
                        order.District = upsertOrderDTO.District;
                        order.AdditionalAdress = upsertOrderDTO.AdditionalAddress;
                        order.CustomerId = upsertOrderDTO.IdentityId;

                        foreach (var item in customerBasket.Items)
                        {
                            if (item.Status == 1)
                            {
                                var orderItem = new OrderItem
                                {
                                    ProductId = item.ProductId,
                                    ProductName = item.ProductName,
                                    Quantity = item.Quantity
                                };
                                order.Items.Add(orderItem);
                            }
                        }

                        // Thêm đơn hàng vào cơ sở dữ liệu
                        var orderResult = await _repositories.AddAsync(order);
                        if (orderResult != null)
                        {
                            // Xóa hết basketItem của khách hàng
                            await ClearCustomerBasket(upsertOrderDTO.IdentityId);
                            upsertOrderResponseDTO.Data = orderResult;
                            upsertOrderResponseDTO.Message = "Add thành công";
                            return upsertOrderResponseDTO;
                        }
                    }
                    else
                    {
                        // Nếu sản phẩm không đủ hàng, báo lỗi cho khách hàng
                        upsertOrderResponseDTO.Message = "Sản phẩm không đủ hàng.";
                        return upsertOrderResponseDTO;
                    }
                }
            }

            upsertOrderResponseDTO.Message = "Thêm đơn hàng thất bại";
            return upsertOrderResponseDTO;
        }

        private async Task<bool> CheckProductsAvailability(List<BasketItem> basketItems)
        {
            // Kiểm tra số lượng sản phẩm có đủ hàng hay không
            foreach (var item in basketItems)
            {
                var product = await _repositories.GetByIdAsync(item.ProductId);
                if (product == null || product.Quantity < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task ClearCustomerBasket(string customerId)
        {
            // Xóa hết basketItem của khách hàng
            var customerBasket = await _repositories.GetByCustomerIdAsync(customerId);
            if (customerBasket != null)
            {
                return await _repositories.DeleteAsync(customerBasket);
            }
        }



        public async Task<Order> DeleteAsync(string orderId)
        {
            return await _repositories.DeleteAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _repositories.GetAllAsync();
        }

        public async Task<Order> GetByCustomerIdAsync(string customerId)
        {
            return await _repositories.GetByCustomerIdAsync(customerId);
        }

        public async Task<Order> GetByIdAsync(string orderId)
        {
            return await _repositories.GetByIdAsync(orderId);
        }
    }
}
