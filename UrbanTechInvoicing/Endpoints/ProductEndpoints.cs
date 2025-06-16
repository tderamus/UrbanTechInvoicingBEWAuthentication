using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/products", async (HttpContext httpContext, IProductService productService) =>
            {
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var products = await productService.GetAllProductsByUserAsync(userId);
                return Results.Ok(products);
            })
                .RequireAuthorization();

            routes.MapGet("/products/{ProductId}", async (Guid ProductId, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(ProductId);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            })
                .RequireAuthorization();

            routes.MapPost("/products", async (HttpContext httpContext, Product product, IProductService productService) =>
            {
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }
                if (product is null)
                {
                    return Results.BadRequest("Product cannot be null.");
                }
                product.CreatorUserId = userId;
                await productService.CreateProductAsync(product);
                return Results.Created($"/products/{product.ProductId}", product);
            })
                .RequireAuthorization();

            routes.MapPut("/products/{ProductId}", async (Guid ProductId, Product product, IProductService productService) =>
            {
                if (product is null)
                {
                    return Results.BadRequest("Product cannot be null.");
                }
                var existingProduct = await productService.GetProductByIdAsync(ProductId);
                if (existingProduct is null)
                {
                    return Results.NotFound();
                }
                await productService.UpdateProductAsync(ProductId, product);
                return Results.Ok(existingProduct);
            })
                .RequireAuthorization();

            routes.MapDelete("/products/{ProductId}", async (Guid ProductId, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(ProductId);
                if (product is null)
                {
                    return Results.NotFound();
                }
                await productService.DeleteProductAsync(ProductId);
                return Results.Content($"ProductID {product.ProductId} has been deleted");
            })
                .RequireAuthorization();

            routes.MapGet("/products/search", async (string searchTerm, IProductService productService) =>
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await productService.GetAllProductsAsync();
                }
                return await productService.GetProductsAsync(searchTerm);
            });
        }

        public static async Task<IResult> GetAllProductsAsync(IProductService productService)
        {
            return TypedResults.Ok(await productService.GetAllProductsAsync());
        }

        public static async Task<IResult> CreateProductAsync(Product product, IProductService productService)
        {
            if (product is null)
            {
                return Results.BadRequest("Product cannot be null.");
            }
            await productService.CreateProductAsync(product);
            return Results.Created($"/products/{product.ProductId}", product);
        }

        public static async Task<IResult> UpdateProductAsync(Guid ProductId, Product product, IProductService productService)
        {
            if (product is null)
            {
                return Results.BadRequest("Product cannot be null.");
            }
            var existingProduct = await productService.GetProductByIdAsync(ProductId);
            if (existingProduct is null)
            {
                return Results.NotFound();
            }
            await productService.UpdateProductAsync(ProductId, product);
            return Results.Ok(existingProduct);
        }

        public static async Task<IResult> DeleteProductAsync(Guid ProductId, IProductService productService)
        {
            var product = await productService.GetProductByIdAsync(ProductId);
            if (product is null)
            {
                return Results.NotFound();
            }
            await productService.DeleteProductAsync(ProductId);
            return Results.Content($"ProductID {product.ProductId} has been deleted");
        }
    }
}