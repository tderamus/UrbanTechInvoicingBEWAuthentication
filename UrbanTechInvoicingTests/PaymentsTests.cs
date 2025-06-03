using System;
using UrbanTechInvoicing.Models;
using Xunit;

namespace UrbanTechInvoicingTests
{
    public class PaymentsTests
    {
        [Fact]
        public void CanCreatePayment_WithValidProperties()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var invoiceId = Guid.NewGuid();
            var payment = new Payments
            {
                PaymentId = paymentId,
                InvoiceId = invoiceId,
                PaymentAmount = 150.50m,
                PaymentDate = new DateTime(2025, 6, 1),
                PaymentType = Payments.PmtType.CreditCard
            };

            // Assert
            Assert.Equal(paymentId, payment.PaymentId);
            Assert.Equal(invoiceId, payment.InvoiceId);
            Assert.Equal(150.50m, payment.PaymentAmount);
            Assert.Equal(new DateTime(2025, 6, 1), payment.PaymentDate);
            Assert.Equal(Payments.PmtType.CreditCard, payment.PaymentType);
        }
    }
}
