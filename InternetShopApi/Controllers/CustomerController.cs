using InternetShopApi.Contracts.Dtos.CustomerDto;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) => _customerService = customerService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllAsync()
        {
            var customer = await _customerService.GetAllCusmoresAsync();
            return Ok(customer);
        }

        [HttpGet("{id}",Name = "GetCustomerById")]
        public async Task<ActionResult<Customer>> GetByIdAsync (int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateAsync (CustomerCreateDto dto)
        {
            try
            {
                var created = await _customerService.CreateCustomerAsync(dto);
                return CreatedAtRoute(
                    "GetCustomerById",
                    new { id = created.CustomerId },
                    created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Customer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _customerService.UpdateCustomerAsync(customer);
                return result ? NoContent() : NotFound();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _customerService.DeleteCustomerAsync(id);
                return result ? NoContent() : NotFound();
            }

            catch (ArgumentNullException)
            {
                return NotFound();
            }

        }
    }
}
