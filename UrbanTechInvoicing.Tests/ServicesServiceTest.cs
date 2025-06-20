using System;
using Moq;
using Xunit;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Services;

namespace UrbanTechInvoicing.Tests
{
    public class ServicesServiceTest
    {
        [Fact]
        public async Task GetAllServicesAsync_ShouldReturnAllServices()
        {
            // Arrange
            var mockServiceRepository = new Mock<IServiceRepository>();
            mockServiceRepository.Setup(repo => repo.GetAllServicesAsync())
                .ReturnsAsync(new List<Service>
                {
                    new Service { ServiceId = Guid.NewGuid(), ServiceName = "Test Service 1", Description = "Description 1" },
                    new Service { ServiceId = Guid.NewGuid(), ServiceName = "Test Service 2", Description = "Description 2" }
                });
            var serviceService = new ServiceService(mockServiceRepository.Object);
            // Act
            var services = await serviceService.GetAllServicesAsync();
            // Assert
            Assert.NotNull(services);
            Assert.Equal(2, services.Count());
        }

        [Fact]
        public async Task CreateServiceAsync_ShouldCreateService()
        {
            // Arrange
            var mockServiceRepository = new Mock<IServiceRepository>();
            var service = new Service
            {
                ServiceId = Guid.NewGuid(),
                ServiceName = "New Service",
                Description = "New Description"
            };

            mockServiceRepository
                .Setup(repo => repo.CreateServiceAsync(It.IsAny<Service>()))
                .ReturnsAsync(service);

            var serviceService = new ServiceService(mockServiceRepository.Object);
            var testUserId = "test-user-123";

            // Act
            await serviceService.CreateServiceAsync(service, testUserId);

            // Assert
            mockServiceRepository.Verify(repo => repo.CreateServiceAsync(It.Is<Service>(
                s => s.CreatorUserId == testUserId
            )), Times.Once);
        }


        [Fact]
        public async Task CreateServiceWithDtoAsync_ShouldReturnCorrectDto()
        {
            // Arrange
            var service = new Service { ServiceId = Guid.NewGuid(), ServiceName = "New", Description = "Desc" };
            var mockRepo = new Mock<IServiceRepository>();
            mockRepo.Setup(r => r.CreateServiceAsync(It.IsAny<Service>())).ReturnsAsync(service);

            var serviceService = new ServiceService(mockRepo.Object);

            // Act
            var dto = await serviceService.CreateServiceWithDtoAsync(service, "user-123");

            // Assert
            Assert.Equal(service.ServiceId, dto.ServiceId);
            Assert.Equal(service.ServiceName, dto.ServiceName);
            Assert.Equal(service.Description, dto.Description);
            Assert.Equal("user-123", dto.CreatorUserId);
        }


        [Fact]
        public async Task UpdateServiceAsync_ShouldUpdateService()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var service = new Service { ServiceId = serviceId, ServiceName = "Updated Service", Description = "Updated Description" };
            var mockServiceRepository = new Mock<IServiceRepository>();
            mockServiceRepository.Setup(repo => repo.GetServiceByIdAsync(serviceId))
                .ReturnsAsync(service);
            mockServiceRepository.Setup(repo => repo.UpdateServiceAsync(serviceId, service))
                .ReturnsAsync(service);
            var serviceService = new ServiceService(mockServiceRepository.Object);
            // Act
            await serviceService.UpdateServiceAsync(serviceId, service);
            // Assert
            mockServiceRepository.Verify(repo => repo.UpdateServiceAsync(serviceId, service), Times.Once);
        }

        [Fact]
        public async Task DeleteServiceAsync_ShouldDeleteService()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var service = new Service { ServiceId = serviceId, ServiceName = "Service to Delete", Description = "Description" };
            var mockServiceRepository = new Mock<IServiceRepository>();
            mockServiceRepository.Setup(repo => repo.GetServiceByIdAsync(serviceId))
                .ReturnsAsync(service);
            mockServiceRepository.Setup(repo => repo.DeleteServiceAsync(serviceId))
                .ReturnsAsync(service);


            var serviceService = new ServiceService(mockServiceRepository.Object);

            // Act
            await serviceService.DeleteServiceAsync(serviceId);

            // Assert
            mockServiceRepository.Verify(repo => repo.DeleteServiceAsync(serviceId), Times.Once);
        }

    }
}
