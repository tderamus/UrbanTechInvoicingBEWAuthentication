using System.Runtime.CompilerServices;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/customers", async (ICustomerService customerService) =>
            {
                return await customerService.GetAllCustomersAsync();
            });

            routes.MapGet("/customers/{CustomerId}", async (Guid CustomerId, ICustomerService customerService) =>
            {
                var customer = await customerService.GetCustomerByIdAsync(CustomerId);
                return customer is not null ? Results.Ok(customer) : Results.NotFound();
            });

            routes.MapPost("/customers", async (Customer customer, ICustomerService customerService) =>
            {
                if (customer is null)
                {
                    return Results.BadRequest("Customer cannot be null.");
                }
                await customerService.CreateCustomerAsync(customer);
                return Results.Created($"/customers/{customer.CustomerId}", customer);
            });

            routes.MapPut("/customers/{CustomerId}", async (Guid CustomerId, Customer customer, ICustomerService customerService) =>
            {
                if (customer is null)
                {
                    return Results.BadRequest("Customer cannot be null.");
                }
                var existingCustomer = await customerService.GetCustomerByIdAsync(CustomerId);
                if (existingCustomer is null)
                {
                    return Results.NotFound();
                }
                await customerService.UpdateCustomerAsync(customer);
                return Results.Ok(existingCustomer);
            });

            routes.MapDelete("/customers/{CustomerId}", async (Guid CustomerId, ICustomerService customerService) =>
            {
                var customer = await customerService.GetCustomerByIdAsync(CustomerId);
                if (customer is null)
                {
                    return Results.NotFound();
                }
                await customerService.DeleteCustomerAsync(CustomerId);
                return Results.NoContent();
            });

            routes.MapGet("/customers/search", async (string searchTerm, ICustomerService customerService) =>
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return Results.BadRequest("Search term cannot be null or empty.");
                }
                var customers = await customerService.SearchCustomersAsync(searchTerm);
                return Results.Ok(customers);
            });
        } 
    }
}
