using System;
using UrbanTechInvoicing.Models;
using Xunit;

namespace UrbanTechInvoicingTests
{
    public class ServiceTests
    {
        [Fact]
        public void CanCreateService_WithValidProperties()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var service = new Service
            {
                ServiceId = serviceId,
                ServiceName = "Consulting",
                Description = "Professional consulting service"
            };

            // Assert
            Assert.Equal(serviceId, service.ServiceId);
            Assert.Equal("Consulting", service.ServiceName);
            Assert.Equal("Professional consulting service", service.Description);
        }
    }
}
