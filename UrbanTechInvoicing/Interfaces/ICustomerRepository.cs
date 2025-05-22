using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(Guid CustomerId);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Guid CustomerId, Customer customer);
        Task<Customer> DeleteCustomerAsync(Guid CustomerId);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    }
}
