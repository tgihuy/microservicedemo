using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Application.Entities;
using OrderServices.Application.Services;
using OrderServices.DTOs;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _services;
        public OrderController(IOrderService services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<ActionResult<Order>> GetAllAsync()
        {
            var item = await _services.GetAllAsync();
            return Ok(item);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetByIdAsync(string id)
        {
            var item = await _services.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpGet("customerId")]
        public async Task<ActionResult<Order>> GetByCustomerIdAsync(string customerId)
        {
            var item = await _services.GetByCustomerIdAsync(customerId);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<UpsertOrderResponseDTO>> PostOrder(UpsertOrderDTO upsertOrderDTO)
        {
            var item = await _services.AddAsync(upsertOrderDTO);
            return Ok(item);
        }


        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            await _services.DeleteAsync(orderId);
            return NoContent();
        }
    }
}
