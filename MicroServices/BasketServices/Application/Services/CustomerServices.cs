using BasketServices.Application.DTOs;
using BasketServices.Application.Entities;
using BasketServices.Application.Repositories.Interface;
using BasketServices.Application.Services.Interface;
using Microservices.Application.Entities;

namespace BasketServices.Application.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerBasketRepositories _repositories;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerServices> _logger;
        private readonly IConfiguration _configuration;
        public CustomerServices(ICustomerBasketRepositories repositories, HttpClient httpClient, ILogger<CustomerServices> logger, IConfiguration configuration)
        {
            _repositories = repositories;
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<UpsertCustomerBasketDTOResponse> AddAsync(UpsertCustomerBasketDTO basket)
        {
            string apiGetProductId = _configuration["HttpGetProduct"] + "/" + basket.ProductId;
            CustomerBasket customerBasket = new CustomerBasket();
            UpsertCustomerBasketDTOResponse upsertCustomerBasketDTOResponse = new UpsertCustomerBasketDTOResponse("", customerBasket);

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await _httpClient.GetAsync(apiGetProductId);
            if (responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.Content.Headers.ContentLength != 0)
                {
                    var product = await responseMessage.Content.ReadFromJsonAsync<ProductDTO>();
                    if (product.AvailableQuantity < basket.Quantity)
                    {
                        upsertCustomerBasketDTOResponse.Data = null;
                        upsertCustomerBasketDTOResponse.Message = "Số lượng sản phẩm không đủ để thêm vào giỏ hàng";
                    }
                    else
                    {
                        var CustomerBasket1 = await _repositories.GetByIdAsync(basket.CustomerId);
                        int remainQuantity = product.AvailableQuantity - basket.Quantity;
                        customerBasket.CustomerId = basket.CustomerId;
                        BasketItem basketItem = new BasketItem()
                        {
                            ProductId = basket.ProductId,
                            ProductName = product.Name,
                            Quantity = basket.Quantity,
                            Status = 1
                        };
                        customerBasket.Items.Add(basketItem);

                        if (CustomerBasket1 != null)
                        {
                            if (CustomerBasket1.Items.Any(e => e.ProductId == basket.ProductId))
                            {
                                for (int i = 0; i < CustomerBasket1.Items.Count; i++)
                                {
                                    if (CustomerBasket1.Items[i].ProductId == basket.ProductId)
                                    {
                                        if (basket.Quantity == 0)
                                        {
                                            upsertCustomerBasketDTOResponse.Data = _repositories.DeleteAsync(basket.CustomerId);
                                            upsertCustomerBasketDTOResponse.Message = "Xóa basket";
                                        }

                                        else
                                        {
                                            var existingItem = CustomerBasket1.Items[i];
                                            existingItem.Quantity += basket.Quantity;
                                            upsertCustomerBasketDTOResponse.Data = _repositories.UpdateQuantityAsync(basket.CustomerId, existingItem.Quantity, basket.ProductId);
                                            upsertCustomerBasketDTOResponse.Message = "Cập nhật basket";
                                        }
                                    }
                                }

                            }
                            else
                            {
                                upsertCustomerBasketDTOResponse.Data = _repositories.AddBasketItem(customerBasket);
                                upsertCustomerBasketDTOResponse.Message = "Thêm mới một basketItem";
                            }
                        }
                        else
                        {
                            upsertCustomerBasketDTOResponse.Data = _repositories.AddCustomerBasketItem(customerBasket);
                            upsertCustomerBasketDTOResponse.Message = "Thêm mới customer basket";
                        }
                    }
                    return upsertCustomerBasketDTOResponse;
                }    
            }
            upsertCustomerBasketDTOResponse.Data = null;
            upsertCustomerBasketDTOResponse.Message = "Them moi thất bại";
            return upsertCustomerBasketDTOResponse;
        }

        public async Task<CustomerBasket> AddBasketItem(CustomerBasket basket)
        {
            return await _repositories.AddBasketItem(basket);
        }

        public async Task<CustomerBasket> DeleteAsync(string customerId)
        {
            return await _repositories.DeleteAsync(customerId);
        }

        public async Task<List<CustomerBasket>> GetAllAsync()
        {
            return await _repositories.GetAllAsync();
        }

        public async Task<CustomerBasket> GetByIdAsync(string id)
        {
            return await _repositories.GetByIdAsync(id);
        }

        public async Task<CustomerBasket> UpdateQuantityAsync(string customerId, int quantity, int productId)
        {
            return await _repositories.UpdateQuantityAsync(customerId, quantity, productId);
        }
    }
}
