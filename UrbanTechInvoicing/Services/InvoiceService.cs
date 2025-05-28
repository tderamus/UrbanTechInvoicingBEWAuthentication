using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            // Await the task to get the actual collection of invoices
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();

            // Get the highest invoice number from the collection
            var lastInvoice = invoices
                .OrderByDescending(i => i.InvoiceNumber)
                .FirstOrDefault();

            int nextNumber = 1;
            if (lastInvoice != null &&
                int.TryParse(lastInvoice.InvoiceNumber.Replace("INV", ""), out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }

            // Format as "INV" + 3-digit number with leading zeros
            invoice.InvoiceNumber = $"INV{nextNumber:D3}";

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

        public async Task<decimal> GetTotalInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            return invoices.Sum(i => i.InvoiceTotal);
        }
    }
}
