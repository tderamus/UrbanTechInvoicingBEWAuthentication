using UrbanTechInvoicing.DTOS;
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

        public async Task<IEnumerable<Payments>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _paymentsRepository.GetPaymentsByUserIdAsync(userId);
        }

        public async Task<Payments> GetPaymentByIdAsync(Guid PaymentId)
        {
            return await _paymentsRepository.GetPaymentByIdAsync(PaymentId);
        }

        public async Task<Payments> CreatePaymentAsync(Payments payment, string ? creatorUserId)
        {
            var result = await _paymentsRepository.CreatePaymentAsync(payment);
            return result;
        }

        public async Task<PaymentDto> CreatePaymentWithDtoAsync(Payments payment, string? creatorUserId)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment), "Payment cannot be null.");
            }
            payment.CreatorUserId = creatorUserId;
            var createdPayment = await _paymentsRepository.CreatePaymentAsync(payment);
            return new PaymentDto(
                createdPayment.PaymentId,
                createdPayment.InvoiceId,
                createdPayment.PaymentAmount,
                createdPayment.PaymentDate,
                createdPayment.PaymentType,
                createdPayment.CreatorUserId);
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
