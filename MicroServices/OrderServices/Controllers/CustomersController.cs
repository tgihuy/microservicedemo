using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderServices.Application.Database;
using OrderServices.Application.Entities;
using OrderServices.Application.Services;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _services;
        public CustomersController(ICustomerServices services)
        {
            _services = services;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var item = await _services.GetByIdentityAsync(id);
            return Ok(item);
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var item = await _services.AddAsync(customer);
            return Ok(item);
        }


    }
}
