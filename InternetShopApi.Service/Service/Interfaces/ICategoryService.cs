using InternetShopApi.Contracts.Dtos.CategoryDto;
using InternetShopApi.Domain.Entities;

namespace InternetShopApi.Service.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResultDto>> GetAllCategoriesAsync();
        Task<CategoryResultDto> GetByIdAsync(int id);
        Task<CategoryResultDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task<CategoryResultDto> UpdateCategory(int id, CategoryCreateDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
