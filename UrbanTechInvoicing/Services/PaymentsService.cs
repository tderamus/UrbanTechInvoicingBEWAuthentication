using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        public PaymentsService(IPaymentsRepository paymentsRepository) => _paymentsRepository = paymentsRepository;

        public async Task<IEnumerable<Payments>> GetAllPaymentsAsync()
        {
            return await _paymentsRepository.GetAllPaymentsAsync();
        }

        public async Task<Payments> GetPaymentByIdAsync(Guid PaymentId)
        {
            return await _paymentsRepository.GetPaymentByIdAsync(PaymentId);
        }

        public async Task<Payments> CreatePaymentAsync(Payments payment)
        {
            await _paymentsRepository.CreatePaymentAsync(payment);
            return payment;
        }

        public async Task<Payments> UpdatePaymentAsync(Guid PaymentId, Payments payment)
        {
            await _paymentsRepository.UpdatePaymentAsync(PaymentId, payment);
            return payment;
        }

        public async Task<Payments> DeletePaymentAsync(Guid PaymentId)
        {
            return await _paymentsRepository.DeletePaymentAsync(PaymentId);  
        }
    }
}





//if (existingPayment == null)
//{
//    return (Payments)Results.BadRequest("Payment cannot be null.");
//}

//if (payment == null)
//{
//    return false;
//}