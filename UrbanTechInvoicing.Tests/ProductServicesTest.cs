using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Xunit;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Endpoints;

namespace UrbanTechInvoicing.Tests
{
    public class ProductServicesTest
    {
        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            productService.Setup(ps => ps.GetAllProductsAsync())
                .ReturnsAsync(new List<Product> { new Product { ProductId = Guid.NewGuid(), ProductName = "Test Product", Description = "Test Sample One" } });
            // Act
            var result = await productService.Object.GetAllProductsAsync();
            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldCreateProduct()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            var newProduct = new Product { ProductId = Guid.NewGuid(), ProductName = "New Product", Description = "Test Sample Two" };
            productService.Setup(ps => ps.CreateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(newProduct);
            // Act
            var result = await productService.Object.CreateProductAsync(newProduct);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(newProduct.ProductId, result.ProductId);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            var existingProduct = new Product { ProductId = Guid.NewGuid(), ProductName = "Existing Product", Description = "Existing Test Sample One" };
            productService.Setup(ps => ps.UpdateProductAsync(It.IsAny<Guid>(), It.IsAny<Product>()))
                .ReturnsAsync(existingProduct);
            // Act
            var result = await productService.Object.UpdateProductAsync(existingProduct.ProductId, existingProduct);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingProduct.ProductId, result.ProductId);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDeleteProduct()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            var productId = Guid.NewGuid();
            productService.Setup(ps => ps.DeleteProductAsync(productId))
                .ReturnsAsync(new Product { ProductId = productId, ProductName = "Deleted Product", Description = "Deleted Test Sample Two" });
            // Act
            var result = await productService.Object.DeleteProductAsync(productId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
        }
    }
}
