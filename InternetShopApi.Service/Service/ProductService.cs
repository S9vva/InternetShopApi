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
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrEmpty(product.Name))
                throw new ArgumentException();
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
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if(string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Product name can't be empty");


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
            if(deleteProduct == null)
                throw new ArgumentNullException(nameof(deleteProduct));

            return await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if(product == null)
                throw new ArgumentNullException(nameof(product));
            if(string.IsNullOrEmpty(product.Name))
                throw new ArgumentException("Product name can't be empty");

            return await _productRepository.UpdateAsync(product);
        }
    }
}
