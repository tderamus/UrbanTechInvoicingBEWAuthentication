using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Dtos;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<IEnumerable<Invoice>> GetInvoiceByCreatorUserIdAsync(string CreatorUserId);
        Task<Invoice> GetInvoiceByIdAsync(Guid InvoiceId);
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        Task<InvoiceDto> CreateInvoiceWithDtoAsync(Invoice invoice, string creatorUserId);
        Task<Invoice> UpdateInvoiceAsync(Guid InvoiceId, Invoice invoice);
        Task<Invoice> DeleteInvoiceAsync(Guid InvoiceId);
        Task<decimal> GetTotalInvoicesAsync();
        Task<Invoice> AddProductsToInvoiceAsync(Guid InvoiceId, IEnumerable<InvoiceProduct> products);
        Task<Invoice> AddServicesToInvoiceAsync(Guid InvoiceId, IEnumerable<InvoiceService> services);
        Task<Invoice> UpdateInvoicePaymentAsync(Guid InvoiceId, InvoicePayments payment);
    }
}
