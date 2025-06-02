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
                .AsNoTracking()
                .Include(i => i.Customer)
                .Include(i => i.InvoicePayments)
                    .ThenInclude(ip => ip.Payment)
                .Include(i => i.InvoiceServices)
                    .ThenInclude(ins => ins.Service)
                .Include(i => i.InvoiceProducts)
                    .ThenInclude(ip => ip.Product)
                .ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid InvoiceId)
        {
            var invoice = await _context.Invoices
                .AsNoTracking()
                .Include(i => i.Customer)
                .Include(i => i.InvoicePayments)
                    .ThenInclude(ip => ip.Payment)
                .Include(i => i.InvoiceServices)
                    .ThenInclude(ins => ins.Service)
                .Include(i => i.InvoiceProducts)
                    .ThenInclude(ip => ip.Product)
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

        public async Task<Invoice> UpdateInvoiceAsync(Guid InvoiceId, Invoice updatedInvoice)
        {
            var existingInvoice = await _context.Invoices
                .Include(i => i.InvoiceProducts)
                .Include(i => i.InvoiceServices)
                .Include(i => i.InvoicePayments)
                .FirstOrDefaultAsync(i => i.InvoiceId == InvoiceId);

            if (existingInvoice == null)
                throw new InvalidOperationException($"Invoice with ID {InvoiceId} not found.");

            // Update scalar properties
            existingInvoice.CustomerId = updatedInvoice.CustomerId;
            existingInvoice.InvoiceNumber = updatedInvoice.InvoiceNumber;
            existingInvoice.InvoiceDate = updatedInvoice.InvoiceDate;
            existingInvoice.DueDate = updatedInvoice.DueDate;
            existingInvoice.InvoiceTotal = updatedInvoice.InvoiceTotal;
            existingInvoice.Status = updatedInvoice.Status;

            // --- Update InvoiceProducts ---
            if (updatedInvoice.InvoiceProducts != null)
            {
                // Remove products not in the updated list
                var toRemove = existingInvoice.InvoiceProducts
                    .Where(ep => !updatedInvoice.InvoiceProducts.Any(np => np.ProductId == ep.ProductId))
                    .ToList();
                foreach (var rem in toRemove)
                    _context.InvoiceProducts.Remove(rem);

                // Add or update products
                foreach (var newProd in updatedInvoice.InvoiceProducts)
                {
                    var existingProd = existingInvoice.InvoiceProducts
                        .FirstOrDefault(ep => ep.ProductId == newProd.ProductId);

                    if (existingProd != null)
                    {
                        existingProd.ProductLineAmount = newProd.ProductLineAmount;
                        existingProd.InvoiceQuantity = newProd.InvoiceQuantity;
                    }
                    else
                    {
                        existingInvoice.InvoiceProducts.Add(new InvoiceProduct
                        {
                            InvoiceId = existingInvoice.InvoiceId,
                            ProductId = newProd.ProductId,
                            ProductLineAmount = newProd.ProductLineAmount,
                            InvoiceQuantity = newProd.InvoiceQuantity
                        });
                    }
                }
            }

            // --- Update InvoiceServices ---
            if (updatedInvoice.InvoiceServices != null)
            {
                var toRemove = existingInvoice.InvoiceServices
                    .Where(es => !updatedInvoice.InvoiceServices.Any(ns => ns.ServiceId == es.ServiceId))
                    .ToList();
                foreach (var rem in toRemove)
                    _context.InvoiceServices.Remove(rem);

                foreach (var newSvc in updatedInvoice.InvoiceServices)
                {
                    var existingSvc = existingInvoice.InvoiceServices
                        .FirstOrDefault(es => es.ServiceId == newSvc.ServiceId);

                    if (existingSvc != null)
                    {
                        existingSvc.ServiceLineAmount = newSvc.ServiceLineAmount;
                        existingSvc.InvoiceQuantity = newSvc.InvoiceQuantity;
                    }
                    else
                    {
                        existingInvoice.InvoiceServices.Add(new InvoiceService
                        {
                            InvoiceId = existingInvoice.InvoiceId,
                            ServiceId = newSvc.ServiceId,
                            ServiceLineAmount = newSvc.ServiceLineAmount,
                            InvoiceQuantity = newSvc.InvoiceQuantity
                        });
                    }
                }
            }

            // --- Update InvoicePayments ---
            if (updatedInvoice.InvoicePayments != null)
            {
                var toRemove = existingInvoice.InvoicePayments
                    .Where(ep => !updatedInvoice.InvoicePayments.Any(np => np.PaymentId == ep.PaymentId))
                    .ToList();
                foreach (var rem in toRemove)
                    _context.InvoicePayments.Remove(rem);

                foreach (var newPay in updatedInvoice.InvoicePayments)
                {
                    var existingPay = existingInvoice.InvoicePayments
                        .FirstOrDefault(ep => ep.PaymentId == newPay.PaymentId);

                    if (existingPay != null)
                    {
                        existingPay.PaymentAmount = newPay.PaymentAmount;
                        existingPay.PaymentDate = newPay.PaymentDate;
                    }
                    else
                    {
                        existingInvoice.InvoicePayments.Add(new InvoicePayments
                        {
                            InvoiceId = existingInvoice.InvoiceId,
                            PaymentId = newPay.PaymentId,
                            PaymentAmount = newPay.PaymentAmount,
                            PaymentDate = newPay.PaymentDate
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return existingInvoice;
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

