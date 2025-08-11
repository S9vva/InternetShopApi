using InternetShopApi.Contracts.Dtos.CustomerDto;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<ActionResult<Customer>> GetByIdAsync (string id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, CustomerUpdateDto dto)
        {

            try
            {
                var result = await _customerService.UpdateCustomerAsync(id, dto);
                if (result == null)
                    return NotFound($"Customer with Id {id} not found.");
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
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
