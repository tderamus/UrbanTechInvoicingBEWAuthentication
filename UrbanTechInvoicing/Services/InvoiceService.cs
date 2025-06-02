using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UrbanTechInvoicing.Services
{
    public class InvoiceServices : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceServices(IInvoiceRepository invoiceRepository) => _invoiceRepository = invoiceRepository;

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

            // Recalculate total from invoice services
            if (invoice.InvoiceServices != null && invoice.InvoiceServices.Any())
            {
                invoice.InvoiceTotal += invoice.InvoiceServices
                    .Sum(s => s.ServiceLineAmount * s.InvoiceQuantity);
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
                var existingProduct = invoice.InvoiceProducts
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (existingProduct != null)
                {
                    existingProduct.InvoiceQuantity += product.InvoiceQuantity;
                    existingProduct.ProductLineAmount += product.ProductLineAmount;
                    existingProduct.ProductLineAmount = existingProduct.ProductLineAmount * existingProduct.InvoiceQuantity;
                    invoice.InvoiceTotal += product.ProductLineAmount * product.InvoiceQuantity;
                }
                else
                {
                    product.InvoiceId = invoiceId;
                    if (product.ProductLineAmount <= 0)
                        throw new InvalidOperationException("Product line amount must be greater than zero.");
                    if (product.InvoiceQuantity <= 0)
                        throw new InvalidOperationException("Invoice quantity must be greater than zero.");

                    invoice.InvoiceTotal += product.ProductLineAmount * product.InvoiceQuantity;
                    invoice.InvoiceProducts.Add(product); 
                }
            }

            return await _invoiceRepository.UpdateInvoiceAsync(invoiceId, invoice);
        }

        public async Task<Invoice> AddServicesToInvoiceAsync(Guid invoiceId, IEnumerable<InvoiceService> services)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
            {
                throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");
            }
            if (invoice.InvoiceServices == null)
            {
                invoice.InvoiceServices = new List<InvoiceService>();
            }
            foreach (var service in services)
            {
                var existingService = invoice.InvoiceServices
                    .FirstOrDefault(s => s.ServiceId == service.ServiceId);

                if (existingService != null)
                {
                    if (service.ServiceLineAmount <= 0)
                        throw new InvalidOperationException("Service line amount must be greater than zero.");
                    if (service.InvoiceQuantity <= 0)
                        throw new InvalidOperationException("Service quantity must be greater than zero.");

                    existingService.InvoiceQuantity += service.InvoiceQuantity;

                    // Assuming ServiceLineAmount is per-unit price
                    existingService.ServiceLineAmount = service.ServiceLineAmount * existingService.InvoiceQuantity;

                    invoice.InvoiceTotal += service.ServiceLineAmount * service.InvoiceQuantity;
                }
                else
                {
                    if (service.ServiceLineAmount <= 0)
                        throw new InvalidOperationException("Service line amount must be greater than zero.");
                    if (service.InvoiceQuantity <= 0)
                        throw new InvalidOperationException("Service quantity must be greater than zero.");

                    var newInvoiceService = new Models.InvoiceService
                    {
                        InvoiceId = invoiceId,
                        ServiceId = service.ServiceId,
                        InvoiceQuantity = service.InvoiceQuantity,
                        ServiceLineAmount = service.ServiceLineAmount * service.InvoiceQuantity // Total line amount
                    };

                    invoice.InvoiceTotal += newInvoiceService.ServiceLineAmount;
                    invoice.InvoiceServices.Add(newInvoiceService);
                }
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
