using UrbanTechInvoicing.DTOS;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces
{
    public interface IPaymentsService
    {
        Task<IEnumerable<Payments>> GetAllPaymentsAsync();
        Task<IEnumerable<Payments>> GetPaymentsByUserIdAsync(string userId);
        Task<Payments> GetPaymentByIdAsync(Guid PaymentId);
        Task<Payments> CreatePaymentAsync(Payments payment, string? creatorUserId);
        Task<Payments> UpdatePaymentAsync(Guid PaymentId, Payments payment);
        Task<Payments> DeletePaymentAsync(Guid PaymentId);
        Task<PaymentDto> CreatePaymentWithDtoAsync(Payments payment, string? creatorUserId);
    }
}
