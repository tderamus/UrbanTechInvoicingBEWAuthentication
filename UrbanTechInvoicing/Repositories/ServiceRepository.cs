using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Data;
using Microsoft.EntityFrameworkCore;

namespace UrbanTechInvoicing.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly UrbanTechDbContext _context;
        public ServiceRepository(UrbanTechDbContext context) => _context = context;
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }
        public async Task<IEnumerable<Service>> GetServicesByUserIdAsync(string userId)
        {
            return await _context.Services.Where(s => s.CreatorUserId == userId).ToListAsync();
        }
        public async Task<Service> GetServiceByIdAsync(Guid ServiceId)
        {
            
            var service  = await _context.Services.FindAsync(ServiceId);
            return service;
        }
        public async Task<Service> CreateServiceAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service;
        }
        public async Task<Service> UpdateServiceAsync(Guid ServiceId, Service service)
        {
            var existingService = await _context.Services.FindAsync(ServiceId);
            if (existingService == null)
            {
                throw new ArgumentException("Service not found.");
            }
            existingService.ServiceName = service.ServiceName;
            existingService.Description = service.Description;
            await _context.SaveChangesAsync();
            return existingService;
        }
        public async Task<Service> DeleteServiceAsync(Guid ServiceId)
        {
            var service = await _context.Services.FindAsync(ServiceId);
            if (service == null)
            {
                throw new ArgumentException("Service not found.");
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return service;
        }
    }
}
