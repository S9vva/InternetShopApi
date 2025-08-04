
using InternetShopApi.Contracts.Dtos.ProductDto;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var product = await _productService.GetAllProductAsync();
            return Ok(product);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            try
            {
                var order = _productService.GetByIdAsync(id);
                return Ok(order);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateAsync(ProductCreateDto dto)
        {
            try
            {

                var created = await _productService.CreateProductAsync(dto);
                return CreatedAtRoute(
                    "GetProductById",
                    new { id = created.ProductId },
                    created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Product product)
        {
            if(id != product.ProductId)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _productService.UpdateProductAsync(product);
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
                var result = await _productService.DeleteProductAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

    }
}
