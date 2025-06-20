using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Dtos;

namespace UrbanTechInvoicing.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository serviceRepository) => _serviceRepository = serviceRepository;
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _serviceRepository.GetAllServicesAsync();
        }
        public async Task<IEnumerable<Service>> GetServicesByUserIdAsync(string userId)
        {
            return await _serviceRepository.GetServicesByUserIdAsync(userId);
        }
        public async Task<Service> GetServiceByIdAsync(Guid ServiceId)
        {
            return await _serviceRepository.GetServiceByIdAsync(ServiceId);
        }
       public async Task<Service> CreateServiceAsync(Service service, string? creatorUserId)
        {
            Console.WriteLine($"📝 Setting CreatorUserId: {creatorUserId}");
            service.CreatorUserId = creatorUserId;

            Console.WriteLine($"➡️ Inserting service: {service.ServiceName}, {service.Description}");
            var result = await _serviceRepository.CreateServiceAsync(service);
            Console.WriteLine("✅ Service inserted successfully.");
            return result;
        }


        public async Task<ServiceDto> CreateServiceWithDtoAsync(Service service, string? creatorUserId)
        {

            var createdService = await CreateServiceAsync(service, creatorUserId);
            return new ServiceDto
            (
                createdService.ServiceId,
                createdService.Description,
                createdService.ServiceName,
                createdService.CreatorUserId
            );

        }
        public async Task UpdateServiceAsync(Guid ServiceId, Service service)
        {
            var existing = await _serviceRepository.GetServiceByIdAsync(ServiceId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Service with ID {ServiceId} not found.");
            }

            existing.ServiceName = service.ServiceName;
            existing.Description = service.Description;

            await _serviceRepository.UpdateServiceAsync(ServiceId, existing);
        }

        public async Task DeleteServiceAsync(Guid ServiceId)
        {
            var existing = await _serviceRepository.GetServiceByIdAsync(ServiceId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Service with ID {ServiceId} not found.");
            }

            await _serviceRepository.DeleteServiceAsync(ServiceId);
        }

    }
}
