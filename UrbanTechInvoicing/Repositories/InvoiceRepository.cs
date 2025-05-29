using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Data;
using Microsoft.EntityFrameworkCore;

using UrbanTechInvoicing.Interfaces;

namespace UrbanTechInvoicing.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly UrbanTechDbContext _context;
        public InvoiceRepository(UrbanTechDbContext context) => _context = context;

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoicePayments)
                .Include(i => i.InvoiceServices)
                .Include(i => i.InvoiceProducts)
                .ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid InvoiceId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoicePayments)
                .Include(i => i.InvoiceServices)
                .Include(i => i.InvoiceProducts)
                .FirstOrDefaultAsync(i => i.InvoiceId == InvoiceId);

            if (invoice == null)
            {
                throw new InvalidOperationException($"Invoice with ID {InvoiceId} not found.");
            }

            return invoice;
        }

        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            var result = await _context.Invoices.AddAsync(invoice);
            if (result.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return invoice;
            }

            throw new InvalidOperationException("Failed to create invoice.");
        }

        public Task<Invoice> UpdateInvoiceAsync(Guid InvoiceId, Invoice invoice)
        {
            var existingInvoice = _context.Invoices.Find(InvoiceId);
            if (existingInvoice != null)
            {
                existingInvoice.CustomerId = invoice.CustomerId;
                existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
                existingInvoice.InvoiceDate = invoice.InvoiceDate;
                existingInvoice.DueDate = invoice.DueDate;
                existingInvoice.InvoiceTotal = invoice.InvoiceTotal;
                existingInvoice.Status = invoice.Status;
                existingInvoice.InvoicePayments = invoice.InvoicePayments;
                existingInvoice.InvoiceServices = invoice.InvoiceServices;
                existingInvoice.InvoiceProducts = invoice.InvoiceProducts;
                _context.Invoices.Update(existingInvoice);
                _context.SaveChanges();
                return Task.FromResult(existingInvoice);
            }
            else
            {
                throw new InvalidOperationException($"Invoice with ID {InvoiceId} not found.");
            }
        }
        public Task<Invoice> DeleteInvoiceAsync(Guid InvoiceId)
        {
            var invoice = _context.Invoices.Find(InvoiceId);
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return Task.FromResult(invoice);
        }
    }
}

