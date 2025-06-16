using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Data;
using Microsoft.EntityFrameworkCore;

namespace UrbanTechInvoicing.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly UrbanTechDbContext _dbContext;

        public ProductRepository(UrbanTechDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByUserAsync(string userId)
        {
            return await _dbContext.Products
                .Where(p => p.CreatorUserId == userId)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid ProductId)
        {
            return await _dbContext.Products.FindAsync(ProductId);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Guid ProductId, Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(ProductId);
            if (existingProduct == null)
            {
                return (Product)Results.BadRequest("Product cannot be null.");
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<Product> DeleteProductAsync(Guid ProductId)
        {
            var product = await _dbContext.Products.FindAsync(ProductId);
            if (product == null)
            {
                return (Product)Results.BadRequest("Product cannot be null.");
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await GetAllProductsAsync();
            }
            return await _dbContext.Products
                .Where(p => p.ProductName.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }

    }
}
