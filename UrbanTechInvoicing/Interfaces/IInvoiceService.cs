using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(Guid InvoiceId);
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        Task<Invoice> UpdateInvoiceAsync(Guid InvoiceId, Invoice invoice);
        Task<Invoice> DeleteInvoiceAsync(Guid InvoiceId);
    }
}
