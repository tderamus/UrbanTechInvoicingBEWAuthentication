using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<IEnumerable<Service>> GetServicesByUserIdAsync(string userId);
        Task<Service> GetServiceByIdAsync(Guid ServiceId);
        Task<Service> CreateServiceAsync(Service service);
        Task<Service> UpdateServiceAsync(Guid ServiceId, Service service);
        Task<Service> DeleteServiceAsync(Guid ServiceId);
    }
}
