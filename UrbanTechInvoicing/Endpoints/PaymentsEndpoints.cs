using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class PaymentsEndpoints
    {
        public static void MapPaymentsEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/payments", async (IPaymentsService paymentsService) =>
            {
                return await paymentsService.GetAllPaymentsAsync();
            });
            routes.MapGet("/payments/{PaymentId}", async (Guid PaymentId, IPaymentsService paymentsService) =>
            {
                var payment = await paymentsService.GetPaymentByIdAsync(PaymentId);
                return payment is not null ? Results.Ok(payment) : Results.NotFound();
            });

            routes.MapPost("/payments", async (Payments payment, IPaymentsService paymentsService) =>
            {
                if (payment is null)
                {
                    return Results.BadRequest("Payment cannot be null.");
                }
                await paymentsService.CreatePaymentAsync(payment);
                return Results.Created($"/payments/{payment.PaymentId}", payment);
            });

            routes.MapPut("/payments/{PaymentId}", async (Guid PaymentId, Payments payment, IPaymentsService paymentsService) =>
            {
                if (payment is null)
                {
                    return Results.BadRequest("Payment cannot be null.");
                }
                //var existingPayment = await paymentsService.GetPaymentByIdAsync(PaymentId);
                //if (existingPayment is null)
                //{
                //    return Results.NotFound();
                //}
                await paymentsService.UpdatePaymentAsync(PaymentId, payment);
                return Results.Ok(payment);
            });

            routes.MapDelete("/payments/{PaymentId}", async (Guid PaymentId, IPaymentsService paymentsService) =>
            {
                var payment = await paymentsService.GetPaymentByIdAsync(PaymentId);
                if (payment is null)
                {
                    return Results.NotFound();
                }
                await paymentsService.DeletePaymentAsync(PaymentId);
                return Results.NoContent();
            });
        }
    }
}
