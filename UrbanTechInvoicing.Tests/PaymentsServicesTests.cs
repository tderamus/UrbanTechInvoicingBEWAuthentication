using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UrbanTechInvoicing.Endpoints;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using Xunit;

namespace UrbanTechInvoicing.Tests
{
    public class PaymentsServicesTests
    {
        [Fact]
        public async Task GetAllPayments_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IPaymentsService>();
            mockService.Setup(s => s.GetAllPaymentsAsync())
                .ReturnsAsync(new List<Payments>
                {
                    new Payments
                    {
                        PaymentId = Guid.NewGuid(),
                        InvoiceId = Guid.NewGuid(),
                        PaymentAmount = 100.00m,
                        PaymentDate = DateTime.UtcNow,
                        PaymentType = Payments.PmtType.Cash
                    }
                });
            // Act
            var result = await PaymentsEndpoints.GetAllPaymentsAsync(mockService.Object);
            // Assert
            var okResult = Assert.IsType<Ok<IEnumerable<Payments>>>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task CreatePayment_NullPayment_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IPaymentsService>();
            Payments? payment = null;
            // Act
            var result = await PaymentsEndpoints.CreatePaymentAsync(payment!, mockService.Object);
            // Assert
            var badRequest = Assert.IsType<BadRequest<string>>(result);
            Assert.Equal("Payment cannot be null.", badRequest.Value);
        }

        [Fact]
        public async Task CreatePayment_ValidPayment_ReturnsCreated()
        {
            // Arrange
            var mockService = new Mock<IPaymentsService>();
            var payment = new Payments
            {
                PaymentId = Guid.NewGuid(),
                InvoiceId = Guid.NewGuid(),
                PaymentAmount = 100.00m,
                PaymentDate = DateTime.UtcNow,
                PaymentType = Payments.PmtType.Cash
            };
            mockService.Setup(s => s.CreatePaymentAsync(payment))
                .ReturnsAsync(payment);
            // Act
            var result = await PaymentsEndpoints.CreatePaymentAsync(payment, mockService.Object);
            // Assert
            var createdResult = Assert.IsType<Created<Payments>>(result);
            Assert.Equal($"/payments/{payment.PaymentId}", createdResult.Location);
            Assert.Equal(payment, createdResult.Value);
        }

        [Fact]
        public async Task UpdatePayment_NullPayment_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IPaymentsService>();
            Payments? payment = null;
            Guid paymentId = Guid.NewGuid();
            // Act
            var result = await PaymentsEndpoints.UpdatePaymentAsync(paymentId, payment!, mockService.Object);
            // Assert
            var badRequest = Assert.IsType<BadRequest<string>>(result);
            Assert.Equal("Payment cannot be null.", badRequest.Value);
        }

        [Fact]
        public async Task UpdatePayment_ValidPayment_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<IPaymentsService>();
            var payment = new Payments
            {
                PaymentId = Guid.NewGuid(),
                InvoiceId = Guid.NewGuid(),
                PaymentAmount = 100.00m,
                PaymentDate = DateTime.UtcNow,
                PaymentType = Payments.PmtType.Cash
            };
            Guid paymentId = payment.PaymentId;
            mockService.Setup(s => s.UpdatePaymentAsync(paymentId, payment))
                .ReturnsAsync(payment);
            // Act
            var result = await PaymentsEndpoints.UpdatePaymentAsync(paymentId, payment, mockService.Object);
            // Assert
            var okResult = Assert.IsType<Ok<Payments>>(result);
            Assert.Equal(payment, okResult.Value);
        }

        [Fact]
        public async Task DeletePayment_NonExistentPayment_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<IPaymentsService>();
            Guid paymentId = Guid.NewGuid();
            mockService.Setup(s => s.GetPaymentByIdAsync(paymentId))
                .ReturnsAsync((Payments?)null);
            // Act
            var result = await PaymentsEndpoints.DeletePaymentAsync(paymentId, mockService.Object);
            // Assert
            Assert.IsType<NotFound>(result);
        }
    }
}
