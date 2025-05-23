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
            await _serviceRepository.UpdateServiceAsync(ServiceId, service);
        }
        public async Task DeleteServiceAsync(Guid ServiceId)
        {
            await _serviceRepository.DeleteServiceAsync(ServiceId);
        }
    }
}
