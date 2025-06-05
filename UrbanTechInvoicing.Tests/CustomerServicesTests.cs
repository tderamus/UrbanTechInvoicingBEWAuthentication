using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UrbanTechInvoicing.Endpoints;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using Xunit;

namespace UrbanTechInvoicing.Tests
{

    public class CustomerServicesTests
    {
        [Fact]
        public async Task GetAllCustomers_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetAllCustomersAsync())
                .ReturnsAsync(new List<Customer>
                {
                new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    Name = "Test",
                    EmailAddress = "test@test.com",
                    PhoneNumber = "123"
                }
                });

            // Act
            var result = await CustomerEndpoints.GetAllCustomers(mockService.Object);

            // Assert
            var okResult = Assert.IsType<Ok<IEnumerable<Customer>>>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task CreateCustomer_NullCustomer_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            Customer? customer = null;

            // Act
            var result = await CustomerEndpoints.CreateCustomer(customer!, mockService.Object);

            // Assert
            var badRequest = Assert.IsType<BadRequest<string>>(result);
            Assert.Equal("Customer cannot be null.", badRequest.Value);
        }


        [Fact]
        public async Task CreateCustomer_ValidCustomer_ReturnsCreated()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Name = "Test",
                EmailAddress = "test@test.com",
                PhoneNumber = "123"
            };

            // Act
            var result = await CustomerEndpoints.CreateCustomer(customer, mockService.Object);

            // Assert
            var createdResult = Assert.IsType<Created<Customer>>(result);
            Assert.Equal(customer, createdResult.Value);
        }

        [Fact]
        public async Task UpdateCustomer_NullCustomer_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            Customer? customer = null; // Explicitly mark the variable as nullable

            // Act
            var result = await CustomerEndpoints.UpdateCustomer(Guid.NewGuid(), customer!, mockService.Object);

            // Assert
            var badRequest = Assert.IsType<BadRequest<string>>(result);
            Assert.Equal("Customer cannot be null.", badRequest.Value);
        }

        [Fact]
        public async Task UpdateCustomer_CustomerNotFound_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomerByIdAsync(It.IsAny<Guid>()));

            var customer = new Customer
            {
                Name = "Test Name",
                EmailAddress = "test@example.com",
                PhoneNumber = "1234567890"
            };

            // Act
            var result = await CustomerEndpoints.UpdateCustomer(Guid.NewGuid(), customer, mockService.Object);

            // Assert
            Assert.IsType<NotFound>(result);
        }


        [Fact]
        public async Task DeleteCustomer_CustomerNotFound_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomerByIdAsync(It.IsAny<Guid>()));


            // Act
            var result = await CustomerEndpoints.DeleteCustomer(Guid.NewGuid(), mockService.Object);

            // Assert
            Assert.IsType<NotFound>(result);
        }
    }
}
