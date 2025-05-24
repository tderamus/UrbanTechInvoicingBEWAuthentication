using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/products", async (IProductService productService) =>
            {
                return await productService.GetAllProductsAsync();
            });

            routes.MapGet("/products/{ProductId}", async (Guid ProductId, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(ProductId);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            routes.MapPost("/products", async (Product product, IProductService productService) =>
            {
                if (product is null)
                {
                    return Results.BadRequest("Product cannot be null.");
                }
                await productService.CreateProductAsync(product);
                return Results.Created($"/products/{product.ProductId}", product);
            });

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
            });

            routes.MapDelete("/products/{ProductId}", async (Guid ProductId, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(ProductId);
                if (product is null)
                {
                    return Results.NotFound();
                }
                await productService.DeleteProductAsync(ProductId);
                return Results.Content($"ProductID {product.ProductId} has been deleted");
            });

            routes.MapGet("/products/search", async (string searchTerm, IProductService productService) =>
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await productService.GetAllProductsAsync();
                }
                return await productService.GetProductsAsync(searchTerm);
            });
        }
    }
}