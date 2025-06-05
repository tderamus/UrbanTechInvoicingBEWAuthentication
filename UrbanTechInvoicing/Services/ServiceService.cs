using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

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
        public async Task<Service> GetServiceByIdAsync(Guid ServiceId)
        {
            return await _serviceRepository.GetServiceByIdAsync(ServiceId);
        }
        public async Task CreateServiceAsync(Service service)
        {
            await _serviceRepository.CreateServiceAsync(service);
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
