using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Dtos;

namespace UrbanTechInvoicing.Endpoints
{
    public static class ServiceEndpoints
    {
        public static void MapServiceEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/services", async (HttpContext httpContext, IServiceService serviceService) =>
            {
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var services = await serviceService.GetServicesByUserIdAsync(userId);
                return services is not null ? Results.Ok(services) : Results.NotFound();
            })
                .RequireAuthorization();

            routes.MapGet("/services/{ServiceId}", async (Guid ServiceId, IServiceService serviceService) =>
            {
                var service = await serviceService.GetServiceByIdAsync(ServiceId);
                return service is not null ? Results.Ok(service) : Results.NotFound();
            })
                .RequireAuthorization();

            routes.MapPost("/services", async (HttpContext httpContext, Service service, IServiceService serviceService) =>
            {

                Console.WriteLine("===== User Claims =====");
                foreach (var claim in httpContext.User.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }
                Console.WriteLine("=======================");

                if (service is null)
                {
                    return Results.BadRequest("Service cannot be null.");
                }

                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(service.ServiceName) || string.IsNullOrWhiteSpace(service.Description))
                {
                    return Results.BadRequest("Service name and description cannot be empty.");
                }
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return Results.Unauthorized();
                }
                try
                    {
                        var serviceDto = await serviceService.CreateServiceWithDtoAsync(service, userId);
                        return Results.Created($"/services/{serviceDto.ServiceId}", serviceDto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"🔥 Error creating service: {ex.Message}");
                        Console.WriteLine(ex.StackTrace);
                        return Results.Problem("Internal server error while creating service.");
                    }
            })
                .RequireAuthorization();

            routes.MapPut("/services/{ServiceId}", async (Guid ServiceId, Service service, IServiceService serviceService) =>
            {
                if (service is null)
                {
                    return Results.BadRequest("Service cannot be null.");
                }
                var existingService = await serviceService.GetServiceByIdAsync(ServiceId);
                if (existingService is null)
                {
                    return Results.NotFound();
                }
                await serviceService.UpdateServiceAsync(ServiceId, service);
                return Results.Ok(existingService);
            })
                .RequireAuthorization();

            routes.MapDelete("/services/{ServiceId}", async (Guid ServiceId, IServiceService serviceService) =>
            {
                var service = await serviceService.GetServiceByIdAsync(ServiceId);
                if (service is null)
                {
                    return Results.NotFound();
                }
                await serviceService.DeleteServiceAsync(ServiceId);
                return Results.NoContent();
            })
                .RequireAuthorization();
        }

        public async static Task<IResult> GetAllServicesAsync(IServiceService serviceService)
        {
            var services = await serviceService.GetAllServicesAsync();
            return Results.Ok(services);
        }

        public async static Task<IResult> CreateServiceAsync(HttpContext context, Service service, IServiceService serviceService)
        {
            if (service is null)
                return Results.BadRequest("Service cannot be null.");

            var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Results.Unauthorized();

            await serviceService.CreateServiceAsync(service, userId);

            return Results.Created($"/services/{service.ServiceId}", service);
        }


        public async static Task<IResult> UpdateServiceAsync(Guid ServiceId, Service service, IServiceService serviceService)
        {
            if (service is null)
            {
                return Results.BadRequest("Service cannot be null.");
            }
            var existingService = await serviceService.GetServiceByIdAsync(ServiceId);
            if (existingService is null)
            {
                return Results.NotFound();
            }
            await serviceService.UpdateServiceAsync(ServiceId, service);
            return Results.Ok(existingService);
        }

        public async static Task<IResult> DeleteServiceAsync(Guid ServiceId, IServiceService serviceService)
        {
            var service = await serviceService.GetServiceByIdAsync(ServiceId);
            if (service is null)
            {
                return Results.NotFound();
            }
            await serviceService.DeleteServiceAsync(ServiceId);
            return Results.NoContent();
        }
    }
}
