using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class ServiceEndpoints
    {
        public static void MapServiceEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/services", async (IServiceService serviceService) =>
            {
                return await serviceService.GetAllServicesAsync();
            });

            routes.MapGet("/services/{ServiceId}", async (Guid ServiceId, IServiceService serviceService) =>
            {
                var service = await serviceService.GetServiceByIdAsync(ServiceId);
                return service is not null ? Results.Ok(service) : Results.NotFound();
            });

            routes.MapPost("/services", async (Service service, IServiceService serviceService) =>
            {
                if (service is null)
                {
                    return Results.BadRequest("Service cannot be null.");
                }
                await serviceService.CreateServiceAsync(service);
                return Results.Created($"/services/{service.ServiceId}", service);
            });

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
            });

            routes.MapDelete("/services/{ServiceId}", async (Guid ServiceId, IServiceService serviceService) =>
            {
                var service = await serviceService.GetServiceByIdAsync(ServiceId);
                if (service is null)
                {
                    return Results.NotFound();
                }
                await serviceService.DeleteServiceAsync(ServiceId);
                return Results.NoContent();
            });

        }   
    }
}
