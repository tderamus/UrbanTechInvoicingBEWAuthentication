using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid CustomerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(CustomerId);
        }
        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.CreateCustomerAsync(customer);
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer.CustomerId, customer);
        }
        public async Task DeleteCustomerAsync(Guid CustomerID)
        {
            await _customerRepository.DeleteCustomerAsync(CustomerID);
        }
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            return await _customerRepository.SearchCustomersAsync(searchTerm);
        }
    }

}
