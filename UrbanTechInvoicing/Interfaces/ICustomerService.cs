using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<Customer>> GetAllCustomersByUserAsync(string userId);
        Task<Customer> GetCustomerByIdAsync(Guid CustomerId);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Guid customerId, Customer customer);
        Task DeleteCustomerAsync(Guid CustomerID);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    }
}
