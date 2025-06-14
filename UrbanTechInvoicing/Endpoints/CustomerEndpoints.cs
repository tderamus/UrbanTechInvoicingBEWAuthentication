using System.Runtime.CompilerServices;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class CustomerEndpoints
    {

        public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/customers", async (HttpContext httpContext, ICustomerService customerService) =>
            {
                // Get logged in user information
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                return await customerService.GetAllCustomersByUserAsync(userId);
            })
            .RequireAuthorization();
            

            routes.MapGet("/customers/{CustomerId}", async (Guid CustomerId, ICustomerService customerService) =>
            {
                var customer = await customerService.GetCustomerByIdAsync(CustomerId);
                return customer is not null ? Results.Ok(customer) : Results.NotFound();
            })
                .RequireAuthorization();

            routes.MapPost("/customers", async (HttpContext httpContext, Customer customer, ICustomerService customerService) =>
            {
                // Get logged in user information
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return Results.Unauthorized();
                }
                if (customer is null)
                {
                    return Results.BadRequest("Customer cannot be null.");
                }
                await customerService.CreateCustomerAsync(customer);
                return Results.Created($"/customers/{customer.CustomerId}", customer);
            })
                .RequireAuthorization();

            routes.MapPut("/customers/{customerId}",
                async (Guid customerId, Customer customer, ICustomerService customerService) =>
                {
                    if (customer is null)
                    {
                        return Results.BadRequest("Customer cannot be null.");
                    }
                    var existingCustomer = await customerService.GetCustomerByIdAsync(customerId);
                    if (existingCustomer is null)
                    {
                        return Results.NotFound();
                    }
                    await customerService.UpdateCustomerAsync(customerId, customer);
                    return Results.Ok(existingCustomer);
                })
                .RequireAuthorization();

            routes.MapDelete("/customers/{CustomerId}", async (Guid CustomerId, ICustomerService customerService) =>
            {
                var customer = await customerService.GetCustomerByIdAsync(CustomerId);
                if (customer is null)
                {
                    return Results.NotFound();
                }
                await customerService.DeleteCustomerAsync(CustomerId);
                return Results.NoContent();
            })
                .RequireAuthorization();

            routes.MapGet("/customers/search", async (string searchTerm, ICustomerService customerService) =>
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return Results.BadRequest("Search term cannot be null or empty.");
                }
                var customers = await customerService.SearchCustomersAsync(searchTerm);
                return Results.Ok(customers);
            })
                .RequireAuthorization();
        }

        public static async Task<IResult> GetAllCustomers(ICustomerService customerService)
        {
            var customers = await customerService.GetAllCustomersAsync();
            return Results.Ok(customers);
        }

        public static async Task<IResult> CreateCustomer(Customer customer, ICustomerService customerService)
        {
            if (customer is null)
            {
                return Results.BadRequest("Customer cannot be null.");
            }
            await customerService.CreateCustomerAsync(customer);
            return Results.Created($"/customers/{customer.CustomerId}", customer);
        }

        public static async Task<IResult> UpdateCustomer(Guid customerId, Customer customer, ICustomerService customerService)
        {
            if (customer is null)
            {
                return Results.BadRequest("Customer cannot be null.");
            }
            var existingCustomer = await customerService.GetCustomerByIdAsync(customerId);
            if (existingCustomer is null)
            {
                return Results.NotFound();
            }
            await customerService.UpdateCustomerAsync(customerId, customer);
            return Results.Ok(existingCustomer);
        }

        public static async Task<IResult> DeleteCustomer(Guid CustomerId, ICustomerService customerService)
        {
            var customer = await customerService.GetCustomerByIdAsync(CustomerId);
            if (customer is null)
            {
                return Results.NotFound();
            }
            await customerService.DeleteCustomerAsync(CustomerId);
            return Results.NoContent();
        }
    }
}
