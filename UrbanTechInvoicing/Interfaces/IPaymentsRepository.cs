using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Interfaces

{
    public interface IPaymentsRepository
    {
        Task<IEnumerable<Payments>> GetAllPaymentsAsync();
        Task<Payments> GetPaymentByIdAsync(int id);
        Task<Payments> CreatePaymentAsync(Payments payment);
        Task<Payments> UpdatePaymentAsync(int id, Payments payment);
        Task<bool> DeletePaymentAsync(int id);
    }
}
