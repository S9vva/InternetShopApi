using InternetShopApi.Contracts.Dtos.CategoryDto;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service.Service;
using InternetShopApi.Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            var categoriesAll = await _categoryService.GetAllCategoriesAsync();
            return Ok(categoriesAll);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetByIdAsync(int id)
        {
            try
            {
                var categoryById = await _categoryService.GetByIdAsync(id);
                return Ok(categoryById);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateAsync(CategoryCreateDto dto)
        {
            try
            {
                var createCategory = await _categoryService.CreateCategoryAsync(dto);
                return CreatedAtRoute(
                    "GetCategoryById",
                    new { id = createCategory.CategoryId },
                    createCategory);
            }
            catch(ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateAsync(int id, Category category)
        {
            if(id != category.CategoryId)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                var result = await _categoryService.UpdateCategory(category);
                return result ? NoContent() : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
        }


    }
    
}
