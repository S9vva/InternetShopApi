using InternetShopApi.Contracts.Dtos.CategoryDto;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service;
using InternetShopApi.Service.Service.Interfaces;


namespace InternetShopApi.Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<IEnumerable<CategoryResultDto>> GetAllCategoriesAsync()
        {
            var category = await _categoryRepository.GetAllAsync();

            var result = category.Select(category => new CategoryResultDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name

            }).ToList();

            return(result);
        }

        public async Task<CategoryResultDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            
            Guard.AgainsNull(category, nameof(category));

            return new CategoryResultDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

        public async Task<CategoryResultDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            Guard.AgainsNull(dto, nameof(dto));

            Guard.AgainstEmpty(dto.Name, nameof(dto.Name));

            var category = new Category
            {
                Name = dto.Name
            };

            var categoryCreate = await _categoryRepository.CreateAsync(category);

            return new CategoryResultDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            Guard.AgainsNull(category, nameof(category));

            return await _categoryRepository.DeleteAsync(id);

        }


        public async Task<bool> UpdateCategory(Category category)
        {
            Guard.AgainsNull(category, nameof(category));
            Guard.AgainstEmpty(category.Name, nameof(category.Name));

            var existingCategory = await _categoryRepository.UpdateAsync(category);
            if (existingCategory == null)
                throw new ArgumentException("Category not found");

            return existingCategory;
        }
    }
}
