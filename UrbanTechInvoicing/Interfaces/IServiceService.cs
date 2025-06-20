using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Dtos;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<IEnumerable<Service>> GetServicesByUserIdAsync(string userId);
        Task<Service> GetServiceByIdAsync(Guid ServiceId);
        Task<Service> CreateServiceAsync(Service service, string? creatorUserId);
        Task<ServiceDto> CreateServiceWithDtoAsync(Service service, string? creatorUserId);
        Task UpdateServiceAsync(Guid ServiceId, Service service);
        Task DeleteServiceAsync(Guid ServiceId);
    }
}
