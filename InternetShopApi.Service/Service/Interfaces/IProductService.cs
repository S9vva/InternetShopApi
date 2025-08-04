using InternetShopApi.Contracts.Dtos.ProductDto;
using InternetShopApi.Domain.Entities;


namespace InternetShopApi.Service.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync();
        Task<ProductResultDto> GetByIdAsync(int id);
        Task<ProductResultDto> CreateProductAsync(ProductCreateDto dto);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
