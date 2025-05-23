using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Data;
using Microsoft.EntityFrameworkCore;

namespace UrbanTechInvoicing.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly UrbanTechDbContext _context;
        public PaymentsRepository(UrbanTechDbContext context) => _context = context;
        public async Task<IEnumerable<Payments>> GetAllPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }
        public async Task<Payments> GetPaymentByIdAsync(Guid PaymentId)
        {
            var payment = await _context.Payments.FindAsync(PaymentId);
            return payment;
        }
        public async Task<Payments> CreatePaymentAsync(Payments payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
        public async Task<Payments> UpdatePaymentAsync(Guid PaymentId, Payments payment)
        {
            var existingPayment = await _context.Payments.FindAsync(PaymentId);

            existingPayment.PaymentAmount = payment.PaymentAmount;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.PaymentType = payment.PaymentType;

            await _context.SaveChangesAsync();
            return existingPayment;
        }
        public async Task<Payments> DeletePaymentAsync(Guid PaymentId)
        {
            var payment = await _context.Payments.FindAsync(PaymentId);
            
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
