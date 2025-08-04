using InternetShopApi.Contracts.Dtos.CategoryDto;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
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
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            return new CategoryResultDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

        public async Task<CategoryResultDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Category name can't be empty");

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

            if (category == null)
                throw new ArgumentNullException(nameof(category));

            return await _categoryRepository.DeleteAsync(id);

        }


        public async Task<bool> UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var existingCategory = await _categoryRepository.UpdateAsync(category);
            if (existingCategory == null)
                throw new ArgumentException("Category not found");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name can't be empty");

            return existingCategory;
        }
    }
}
