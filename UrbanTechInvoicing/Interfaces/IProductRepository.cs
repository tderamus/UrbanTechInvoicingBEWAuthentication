using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsByUserAsync(string userId);
        Task<Product> GetProductByIdAsync(Guid ProductId);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Guid ProductId, Product product);
        Task<Product> DeleteProductAsync(Guid ProductId);
        Task<IEnumerable<Product>> GetProductsAsync(string searchTerm);
    }
}
