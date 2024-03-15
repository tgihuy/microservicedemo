using System.Text;
using System.Text.Json;
using Microsoft.Build.Evaluation;
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
            string apiGetBasketId = _configuration["HttpGetCustomerBasket"] + "/" + upsertOrderDTO.CustomerId;
            Order order = new Order();
            UpsertOrderResponseDTO upsertOrderResponseDTO = new UpsertOrderResponseDTO("", order);
            List<ProductUpdateQuantity> productUpdateQuantities = new List<ProductUpdateQuantity>();

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(apiGetBasketId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var customerBasket = await responseMessage.Content.ReadFromJsonAsync<CustomerBasket>();
                if (customerBasket != null && customerBasket.Items.Any())
                {
                    order.OrderDate = DateTime.Now;
                    order.Street = upsertOrderDTO.Street;
                    order.District = upsertOrderDTO.District;
                    order.City = upsertOrderDTO.City;
                    order.AdditionalAdress = upsertOrderDTO.AdditionalAddress;
                    order.CustomerId = upsertOrderDTO.CustomerId;
                    foreach (var item in customerBasket.Items)
                    {
                        if (item.Status == 1)
                        {
                            HttpResponseMessage productResponse = await _httpClient.GetAsync($"http://localhost:5167/api/Product/productItem/{item.ProductId}");
                            if (productResponse.IsSuccessStatusCode)
                            {
                                var product = await productResponse.Content.ReadFromJsonAsync<ProductDTO>();
                                if (product != null && product.AvailableQuantity >= item.Quantity)
                                {
                                    var orderItem = new OrderItem
                                    {
                                        ProductId = item.ProductId,
                                        ProductName = product.Name,
                                        Quantity = item.Quantity
                                    };
                                    order.Items.Add(orderItem);

                                    productUpdateQuantities.Add(new ProductUpdateQuantity
                                    {
                                        ProductId = item.ProductId,
                                        Quantity = product.AvailableQuantity - item.Quantity
                                    });
                                }
                                else
                                {
                                    upsertOrderResponseDTO.Data = null;
                                    upsertOrderResponseDTO.Message = $"Sản phẩm '{product?.Name}' không đủ hàng";
                                    return upsertOrderResponseDTO;
                                }
                            }
                            else
                            {
                                return new UpsertOrderResponseDTO("Lỗi khi gọi microservice", null);
                            }
                        }
                    }


                    var orderResult = await _repositories.AddAsync(order);
                    if (orderResult != null)
                    {
                        UpdateProductQuantity(productUpdateQuantities);

                        HttpResponseMessage deleteBasketResponse = await _httpClient.DeleteAsync($"http://localhost:5212/api/Basket/{upsertOrderDTO.CustomerId}");
                        if (!deleteBasketResponse.IsSuccessStatusCode)
                        {
                            return new UpsertOrderResponseDTO("Lỗi khi xóa giỏ hàng", null);
                        }

                        upsertOrderResponseDTO.Data = orderResult;
                        upsertOrderResponseDTO.Message = "Add thành công";
                        return upsertOrderResponseDTO;
                    }


                }
            }
            upsertOrderResponseDTO.Message = "Add thất bại";
            return upsertOrderResponseDTO;
        }
        public void UpdateProductQuantity(List<ProductUpdateQuantity> listProductUpdateQuantity)
        {
            try
            {
                for (int i = 0; i < listProductUpdateQuantity.Count; i++)
                {
                    // Gọi hàm PATCH
                    PatchData(listProductUpdateQuantity[i].ProductId, listProductUpdateQuantity[i]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async Task PatchData(int resourceId, ProductUpdateQuantity updateData)
        {

            // Tạo URL PATCH với ID của đối tượng cần cập nhật
            string patchUrl = _configuration["HttpGetProduct"] + "/ProductQuantity/" + resourceId;
            ProductDTO product = GetQuantityByProductId(resourceId).Result;

            if (product != null)
            {
                var updateQuantityData = new ProductUpdateQuantity()
                {
                    ProductId = updateData.ProductId,
                    Quantity = updateData.Quantity,
                };

                // Chuyển đối tượng UpdateData thành chuỗi JSON
                string jsonData = JsonSerializer.Serialize(updateQuantityData);

                // Tạo nội dung PATCH request
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Thực hiện PATCH request
                _httpClient.PatchAsync(patchUrl, content);
            }
        }

        public async Task<ProductDTO> GetQuantityByProductId(int id)
        {

            string ApiGetProductById = _configuration["HttpGetProduct"] + "/productItem/" + id;
            HttpResponseMessage response = new HttpResponseMessage();

            response = await _httpClient.GetAsync(ApiGetProductById);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content.Headers.ContentLength != 0)
                    {
                        var product = await response.Content.ReadFromJsonAsync<ProductDTO>();
                        return product;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
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
