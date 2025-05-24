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
        }
    }
}
