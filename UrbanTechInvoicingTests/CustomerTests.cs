using System;
using UrbanTechInvoicing.Models;
using Xunit;

namespace UrbanTechInvoicingTests
{
    public class CustomerTests
    {
        [Fact]
        public void CanCreateCustomer_WithValidProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var customer = new Customer
            {
                CustomerId = id,
                Name = "Test Customer",
                EmailAddress = "test@example.com",
                PhoneNumber = "1234567890"
            };

            // Assert
            Assert.Equal(id, customer.CustomerId);
            Assert.Equal("Test Customer", customer.Name);
            Assert.Equal("test@example.com", customer.EmailAddress);
            Assert.Equal("1234567890", customer.PhoneNumber);
        }
    }
}
