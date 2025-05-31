using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Interfaces;

namespace UrbanTechInvoicing.Endpoints
{
    public static class InvoiceEndpoints
    {
        public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/invoices", async (IInvoiceService invoiceService) =>
            {
                return await invoiceService.GetAllInvoicesAsync();
            });
            routes.MapGet("/invoices/{InvoiceId}", async (Guid InvoiceId, IInvoiceService invoiceService) =>
            {
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            });
            routes.MapPost("/invoices", async (Invoice invoice, IInvoiceService invoiceService) =>
            {
                if (invoice is null)
                {
                    return Results.BadRequest("Invoice cannot be null.");
                }
                await invoiceService.CreateInvoiceAsync(invoice);
                return Results.Created($"/invoices/{invoice.InvoiceId}", invoice);
            });
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
            });
            routes.MapDelete("/invoices/{InvoiceId}", async (Guid InvoiceId, IInvoiceService invoiceService) =>
            {
                var invoice = await invoiceService.GetInvoiceByIdAsync(InvoiceId);
                if (invoice is null)
                {
                    return Results.NotFound();
                }
                await invoiceService.DeleteInvoiceAsync(InvoiceId);
                return Results.NoContent();
            });

            routes.MapGet("/invoices/total", async (IInvoiceService invoiceService) =>
            {
                return await invoiceService.GetTotalInvoicesAsync();
            });

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
                invoice.InvoiceProducts.Add(product);
                await invoiceService.UpdateInvoiceAsync(InvoiceId, invoice);
                return Results.Ok(invoice);
            });

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
                invoice.InvoiceServices.Add(service);
                await invoiceService.UpdateInvoiceAsync(InvoiceId, invoice);
                return Results.Ok(invoice);
            });

            // Add invoicepayments to an invoice and update invoice payment status
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
            });

        }
    }
}
