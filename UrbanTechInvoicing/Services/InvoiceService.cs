using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Interfaces;

namespace UrbanTechInvoicing.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository) => _invoiceRepository = invoiceRepository;

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllInvoicesAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid InvoiceId)
        {
            return await _invoiceRepository.GetInvoiceByIdAsync(InvoiceId);
        }

        // Create a new invoice and auto increment Invoice Number
        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            var maxInvoiceNumber = invoices.Max(i => i.InvoiceNumber);
            invoice.InvoiceNumber = maxInvoiceNumber + 1;
            return await _invoiceRepository.CreateInvoiceAsync(invoice);
        }

        public async Task<Invoice> UpdateInvoiceAsync(Guid InvoiceId, Invoice invoice)
        {
            return await _invoiceRepository.UpdateInvoiceAsync(InvoiceId, invoice);
        }

        public async Task<Invoice> DeleteInvoiceAsync(Guid InvoiceId)
        {
            return await _invoiceRepository.DeleteInvoiceAsync(InvoiceId);
        }
    }
}
