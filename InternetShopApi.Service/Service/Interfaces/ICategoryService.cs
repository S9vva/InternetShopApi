using InternetShopApi.Contracts.Dtos.CategoryDto;
using InternetShopApi.Domain.Entities;

namespace InternetShopApi.Service.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResultDto>> GetAllCategoriesAsync();
        Task<CategoryResultDto> GetByIdAsync(int id);
        Task<CategoryResultDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
