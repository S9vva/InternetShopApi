using InternetShopApi.Contracts.Dtos.OrderDto;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Dtos.OrderDto;
using InternetShopApi.Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) => _orderService = orderService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            var order = await _orderService.GetAllOrderAsync();

            return Ok(order);                                                                               
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<Order>> GetByIdAsync(int id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                return Ok(order);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateAsync(OrderCreateDto dto)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(dto);

                return CreatedAtRoute(
                    "GetOrderById",
                    new { id = result.OrderId },
                    result  );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var result = await _orderService.UpdateOrderAsync(order);
                return result ? NoContent() : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _orderService.DeleteOrderAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }
    }
}
