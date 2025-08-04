using Humanizer;
using InternetShopApi.Contracts.Dtos.ProductDto;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;

using InternetShopApi.Service.Service.Interfaces;

namespace InternetShopApi.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(x => new ProductResultDto
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name
            }).ToList();
        }

        public async Task<ProductResultDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            Guard.AgainsNull(product, nameof(product));

            return new ProductResultDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
        }
        public async Task<ProductResultDto> CreateProductAsync(ProductCreateDto dto)
        {
            Guard.AgainsNull(dto, nameof(dto));
            Guard.AgainstEmpty(dto.Name, nameof(dto.Name));


            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };

            var productCreate = await _productRepository.CreateAsync(product);

            return new ProductResultDto
            {
                Name = productCreate.Name,
                Price = productCreate.Price,
                CategoryId = productCreate.CategoryId
            };
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var deleteProduct = await _productRepository.GetByIdAsync(id);
            Guard.AgainsNull(deleteProduct, nameof(deleteProduct));

            return await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            Guard.AgainsNull(product, nameof(product));
            Guard.AgainstEmpty(product.Name, nameof(product.Name));

            return await _productRepository.UpdateAsync(product);
        }
    }
}
