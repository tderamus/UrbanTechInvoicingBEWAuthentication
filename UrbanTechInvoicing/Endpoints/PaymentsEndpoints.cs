using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.DTOS;


namespace UrbanTechInvoicing.Endpoints
{
    public static class PaymentsEndpoints
    {
        public static void MapPaymentsEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/payments", async (HttpContext httpContext, IPaymentsService paymentsService) =>
            {
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }
                var payments = await paymentsService.GetPaymentsByUserIdAsync(userId);
                return payments.Any() ? Results.Ok(payments) : Results.NotFound("No payments found for the user.");
            })
                .RequireAuthorization();

            routes.MapGet("/payments/{PaymentId}", async (Guid PaymentId, IPaymentsService paymentsService) =>
            {
                var payment = await paymentsService.GetPaymentByIdAsync(PaymentId);
                return payment is not null ? Results.Ok(payment) : Results.NotFound();
            })
                .RequireAuthorization();

            routes.MapPost("/payments", async (HttpContext httpContext, PaymentCreateDto dto, IPaymentsService paymentsService) =>
            {
                if (dto is null)
                {
                    return Results.BadRequest("Payment cannot be null.");
                }
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }
                var payment = new Payments
                {
                    InvoiceId = dto.InvoiceId,
                    PaymentAmount = dto.PaymentAmount,
                    PaymentDate = dto.PaymentDate,
                    PaymentType = dto.PaymentType,
                    CreatorUserId = userId
                };

                var paymentDto = await paymentsService.CreatePaymentWithDtoAsync(payment, userId);
                return Results.Created($"/payments/{paymentDto.PaymentId}", paymentDto);
            })
                .RequireAuthorization();

            routes.MapPut("/payments/{PaymentId}", async (Guid PaymentId, Payments payment, IPaymentsService paymentsService) =>
            {
                if (payment is null)
                {
                    return Results.BadRequest("Payment cannot be null.");
                }
                
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

        public static async Task<IResult> GetAllPaymentsAsync(IPaymentsService paymentsService)
        {
            return TypedResults.Ok(await paymentsService.GetAllPaymentsAsync());
        }

        public static async Task<IResult> CreatePaymentAsync(HttpContext context, Payments payment, IPaymentsService paymentsService)
        {
            if (payment is null)
            {
                return TypedResults.BadRequest("Payment cannot be null.");
            }
            var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return TypedResults.Unauthorized();
            }
            var createdPayment = await paymentsService.CreatePaymentWithDtoAsync(payment, userId);
            return TypedResults.Created($"/payments/{createdPayment.PaymentId}", createdPayment);
        }

        public static async Task<IResult> UpdatePaymentAsync(Guid PaymentId, Payments payment, IPaymentsService paymentsService)
        {
            if (payment is null)
            {
                return TypedResults.BadRequest("Payment cannot be null.");
            }
            var updated = await paymentsService.UpdatePaymentAsync(PaymentId, payment);
            return TypedResults.Ok(updated);
        }

        public static async Task<IResult> DeletePaymentAsync(Guid PaymentId, IPaymentsService paymentsService)
        {
            var existing = await paymentsService.GetPaymentByIdAsync(PaymentId);
            if (existing is null)
            {
                return TypedResults.NotFound();
            }

            await paymentsService.DeletePaymentAsync(PaymentId);
            return TypedResults.NoContent();
        }
    }
}
