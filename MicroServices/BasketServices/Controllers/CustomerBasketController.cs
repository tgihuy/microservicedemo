using BasketServices.Application.DTOs;
using BasketServices.Application.Entities;
using BasketServices.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketServices.Controllers
{
    [Route("api/Basket")]
    [ApiController]
    public class CustomerBasketController : ControllerBase
    {
        private readonly ICustomerServices _services;
        public CustomerBasketController(ICustomerServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetAllAsync()
        {
            var item = await _services.GetAllAsync();
            return Ok(item);
        }

        [HttpGet("id")]
        public async Task<ActionResult<CustomerBasket>> GetByIdAsync(string id)
        {
            var item = await _services.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<UpsertCustomerBasketDTOResponse> AddBasket(UpsertCustomerBasketDTO basket)
        {
            return await _services.AddAsync(basket);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult<CustomerBasket>> PutStudent(string customerId, UpdateBasketItemDto basketItem)
        {
            var item = await _services.UpdateQuantityAsync(customerId, basketItem.Quantity, basketItem.ProductId);
            return Ok(item);
        }


        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerBasket(string customerId)
        {
            await _services.DeleteAsync(customerId);
            return NoContent();
        }
    }
}
