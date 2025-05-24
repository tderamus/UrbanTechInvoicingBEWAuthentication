using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Interfaces;

namespace UrbanTechInvoicing.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<Product> GetProductByIdAsync(Guid ProductId)
        {
            return await _productRepository.GetProductByIdAsync(ProductId);
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.CreateProductAsync(product);
        }
        public async Task<Product> UpdateProductAsync(Guid ProductId, Product product)
        {
            return await _productRepository.UpdateProductAsync(ProductId, product);
        }
        public async Task<Product> DeleteProductAsync(Guid ProductId)
        {
            return await _productRepository.DeleteProductAsync(ProductId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await GetAllProductsAsync();
            }
            return await _productRepository.GetProductsAsync(searchTerm);
        }
    }
    
}
