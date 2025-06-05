
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using UrbanTechInvoicing.Data;
using UrbanTechInvoicing.Repositories;
using UrbanTechInvoicing.Models;


namespace UrbanTechInvoicing.Tests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var mockContext = new Mock<UrbanTechDbContext>();
            var mockSet = new Mock<DbSet<Customer>>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var repository = new CustomerRepository(mockContext.Object);
            // Act
            var result = await repository.GetAllCustomersAsync();
            // Assert
            Assert.NotNull(result);
            //mockSet.Verify(m => m.ToListAsync(), Times.Once);
        }
    }
}
