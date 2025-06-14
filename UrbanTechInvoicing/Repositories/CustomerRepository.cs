using Microsoft.EntityFrameworkCore;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Data;

namespace UrbanTechInvoicing.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UrbanTechDbContext _context;
        public CustomerRepository(UrbanTechDbContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersByUserAsync(string userId)
        {
            return await _context.Customers
                .Where(c => c.CreatorUserId == userId)
                .ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid CustomerId)
        {
            var customer = await _context.Customers.FindAsync(CustomerId);
            if (customer == null)
            {
                return (Customer)Results.BadRequest("Invalid customer ID.");
            }
            //var customer = await _context.Customers.FindAsync(CustomerId);
            if (customer == null)
            {
                return (Customer)Results.NotFound("Customer not found.");
            }
            return customer;

        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                return (Customer)Results.BadRequest("Customer cannot be null.");
            }
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> UpdateCustomerAsync(Guid CustomerId, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(CustomerId);
            if (existingCustomer == null)
            {
                return (Customer)Results.BadRequest("Customer cannot be null.");
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            await _context.SaveChangesAsync();
            return existingCustomer;
        }
        public async Task<Customer> DeleteCustomerAsync(Guid CustomerId)
        {
            var customer = await _context.Customers.FindAsync(CustomerId);
            if (customer == null)
            {
                return (Customer)Results.NotFound("Customer not found.");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await GetAllCustomersAsync();
            }
            return await _context.Customers
                .Where(c => c.Name.Contains(searchTerm) || c.EmailAddress.Contains(searchTerm) || c.PhoneNumber.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
