using Microsoft.AspNetCore.Http;
using UrbanTechInvoicing.Dtos;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class InvoiceEndpoints
    {
        public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/invoices", async (HttpContext httpcontext, IInvoiceService invoiceService) =>
            {
                var userId = httpcontext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }
                var invoices = await invoiceService.GetInvoiceByCreatorUserIdAsync(userId);
                return Results.Ok(invoices);
            })
                .RequireAuthorization();

            routes.MapGet("/invoices/{InvoiceId}", async (Guid InvoiceId, IInvoiceService invoiceService) =>
            {
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            })
                .RequireAuthorization();

            routes.MapPost("/invoices", async (HttpContext httpContext, CreateInvoiceDto dto,  IInvoiceService invoiceService) =>
            {
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }
                if (dto is null)
                {
                    return Results.BadRequest("Invoice data cannot be null.");
                }

                var invoice = new Invoice
                {
                    CreatorUserId = userId,
                    CustomerId = dto.CustomerId,
                    InvoiceDate = dto.InvoiceDate,
                    DueDate = dto.DueDate,
                    Status = dto.Status,
                    InvoiceTotal = dto.InvoiceTotal
                };

                var invoiceDto = await invoiceService.CreateInvoiceWithDtoAsync(invoice, userId);
                return Results.Created($"/invoices/{invoiceDto.InvoiceId}", invoiceDto);
            })
                .RequireAuthorization();

            routes.MapPut("/invoices/{InvoiceId}", async (Guid InvoiceId, Invoice invoice, IInvoiceService invoiceService) =>
            {
                if (invoice is null)
                {
                    return Results.BadRequest("Invoice cannot be null.");
                }
                var existingInvoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                if (existingInvoice is null)
                {
                    return Results.NotFound();
                }
                await invoiceService.UpdateInvoiceAsync(InvoiceId, invoice);
                return Results.Ok(existingInvoice);
            })
                .RequireAuthorization();

            routes.MapDelete("/invoices/{InvoiceId}", async (Guid InvoiceId, IInvoiceService invoiceService) =>
            {
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                if (invoice is null)
                {
                    return Results.NotFound();
                }
                await invoiceService.DeleteInvoiceAsync(InvoiceId);
                return Results.NoContent();
            })
                .RequireAuthorization();

            routes.MapGet("/invoices/total", async (IInvoiceService invoiceService) =>
            {
                return Results.Ok(await invoiceService.GetTotalInvoicesAsync());
            })
                .RequireAuthorization();

            // Add products to an invoice
            routes.MapPost("/invoices/{InvoiceId}/products", async (
                Guid InvoiceId,
                InvoiceProduct product,
                IInvoiceService invoiceService) =>
            {
                if (product is null)
                {
                    return Results.BadRequest("Product cannot be null.");
                }
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                if (invoice is null)
                {
                    return Results.NotFound();
                }
                invoice.InvoiceProducts ??= new List<InvoiceProduct>();
                var existingProduct = invoice.InvoiceProducts
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.InvoiceQuantity += product.InvoiceQuantity;
                    existingProduct.ProductLineAmount = product.ProductLineAmount;
                }
                else
                {
                    product.InvoiceId = InvoiceId;
                    invoice.InvoiceProducts.Add(product);
                }
                await invoiceService.UpdateInvoiceAsync(InvoiceId, invoice);
                return Results.Ok(invoice);
            })
                .RequireAuthorization();

            // Add services to an invoice
            routes.MapPost("/invoices/{InvoiceId}/services", async (
                Guid InvoiceId,
                InvoiceService service,
                IInvoiceService invoiceService) =>
            {
                if (service is null)
                {
                    return Results.BadRequest("Service cannot be null.");
                }
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                if (invoice is null)
                {
                    return Results.NotFound();
                }
                invoice.InvoiceServices ??= new List<InvoiceService>();
                var existingService = invoice.InvoiceServices
                    .FirstOrDefault(s => s.ServiceId == service.ServiceId);
                if (existingService != null)
                {
                    existingService.InvoiceQuantity += service.InvoiceQuantity;
                    existingService.ServiceLineAmount = service.ServiceLineAmount;
                }
                else
                {
                    service.InvoiceId = InvoiceId;
                    invoice.InvoiceServices.Add(service);
                }
                await invoiceService.UpdateInvoiceAsync(InvoiceId, invoice);
                return Results.Ok(invoice);
            })
                .RequireAuthorization();

            // Add invoice payments to an invoice and update invoice payment status
            routes.MapPost("/invoices/{InvoiceId}/payments", async (
                Guid InvoiceId,
                InvoicePayments payment,
                IInvoiceService invoiceService) =>
            {
                if (payment is null)
                {
                    return Results.BadRequest("Payment cannot be null.");
                }
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                if (invoice is null)
                {
                    return Results.NotFound();
                }
                invoice.InvoicePayments ??= new List<InvoicePayments>();
                invoice.InvoicePayments.Add(payment);
                await invoiceService.UpdateInvoicePaymentAsync(InvoiceId, payment);
                return Results.Ok(invoice);
            })
                .RequireAuthorization();
        }
    }
}
