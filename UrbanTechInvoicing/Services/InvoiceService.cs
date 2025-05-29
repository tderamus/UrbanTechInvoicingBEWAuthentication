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
            // Recalculate total from invoice products
            if (invoice.InvoiceProducts != null && invoice.InvoiceProducts.Any())
            {
                invoice.InvoiceTotal = invoice.InvoiceProducts
                    .Sum(p => p.ProductLineAmount * p.InvoiceQuantity);
            }
            else
            {
                invoice.InvoiceTotal = 0;
            }

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

        // Add products to an invoice
        public async Task<Invoice> AddProductsToInvoiceAsync(Guid invoiceId, IEnumerable<InvoiceProduct> products)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
            {
                throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");
            }
            if (invoice.InvoiceProducts == null)
            {
                invoice.InvoiceProducts = new List<InvoiceProduct>();
            }
            foreach (var product in products)
            {
                invoice.InvoiceProducts.Add(product);
            }
            return await _invoiceRepository.UpdateInvoiceAsync(invoiceId, invoice);
        }

        // Update Invoice Payment and Status
        public async Task<Invoice> UpdateInvoicePaymentAsync(Guid invoiceId, InvoicePayments payment)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");

            invoice.InvoicePayments ??= new List<InvoicePayments>();

            // Check if the payment already exists (e.g., based on Id)
            var existingPayment = invoice.InvoicePayments.FirstOrDefault(p => p.PaymentId == payment.PaymentId);
            if (existingPayment != null)
            {
                existingPayment.PaymentAmount = payment.PaymentAmount;
                existingPayment.PaymentDate = payment.PaymentDate;
            }
            else
            {
                invoice.InvoicePayments.Add(payment);
            }

            // Recalculate payment total and update status
            var totalPayments = invoice.InvoicePayments.Sum(p => p.PaymentAmount);
            var totalInvoiceAmount = invoice.InvoiceProducts?
                .Sum(p => p.ProductLineAmount * p.InvoiceQuantity) ?? 0;

            invoice.InvoiceTotal = totalInvoiceAmount - totalPayments;

            // Optional status logic
            if (invoice.InvoiceTotal <= 0)
                invoice.Status = Invoice.InvoiceStatus.Paid;
            else
                invoice.Status = Invoice.InvoiceStatus.PartiallyPaid;

            return await _invoiceRepository.UpdateInvoiceAsync(invoiceId, invoice);
        }

    }
}
