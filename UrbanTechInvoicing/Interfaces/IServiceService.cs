using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Models.Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(Guid ServiceId);
        Task CreateServiceAsync(Service service);
        Task UpdateServiceAsync(Guid ServiceId, Service service);
        Task DeleteServiceAsync(Guid ServiceId);
    }
}
