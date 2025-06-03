using System;
using UrbanTechInvoicing.Models;
using Xunit;

namespace UrbanTechInvoicingTests
{
    public class ProductTests
    {
        [Fact]
        public void CanCreateProduct_WithValidProperties()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                ProductId = productId,
                ProductName = "Widget",
                Description = "A useful widget"
            };

            // Assert
            Assert.Equal(productId, product.ProductId);
            Assert.Equal("Widget", product.ProductName);
            Assert.Equal("A useful widget", product.Description);
        }
    }
}
