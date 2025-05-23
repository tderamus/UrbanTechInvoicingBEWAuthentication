using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces

{
    public interface IPaymentsRepository
    {
        Task<IEnumerable<Payments>> GetAllPaymentsAsync();
        Task<Payments> GetPaymentByIdAsync(Guid PaymentId);
        Task<Payments> CreatePaymentAsync(Payments payment);
        Task<Payments> UpdatePaymentAsync(Guid PaymentId, Payments payment);
        Task<Payments> DeletePaymentAsync(Guid PaymentId);
    }
}
