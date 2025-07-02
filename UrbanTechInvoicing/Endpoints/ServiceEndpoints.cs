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

                    if (string.IsNullOrEmpty(userId))
                        return Results.Unauthorized();

                    var services = await serviceService.GetServicesByUserIdAsync(userId);
                    return Results.Ok(services);
                })
                .RequireAuthorization();

            routes.MapPost("/services", async (HttpContext httpContext, ServiceCreateDto dto, IServiceService serviceService) =>
                {
                    if (dto is null)
                        return Results.BadRequest("Service cannot be null.");

                    var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    Console.WriteLine($"[Endpoint] userId from claims: {userId}");
                    if (string.IsNullOrWhiteSpace(userId))
                        return Results.Unauthorized();
                    if (string.IsNullOrWhiteSpace(dto.ServiceName) || string.IsNullOrWhiteSpace(dto.Description))
                        return Results.BadRequest("Service name and description cannot be empty.");
                    

                    var service = new Service
                    {
                        ServiceName = dto.ServiceName,
                        Description = dto.Description,
                        CreatorUserId = userId
                    };

                    var serviceDto = await serviceService.CreateServiceWithDtoAsync(service, userId);
                    return Results.Created($"/services/{serviceDto.ServiceId}", serviceDto);
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
