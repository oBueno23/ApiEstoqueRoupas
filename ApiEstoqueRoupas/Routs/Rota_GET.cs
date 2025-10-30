using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_GET
{
    public static void MapGetRoutes(this WebApplication app, InventoryStore store)
    {
        app.MapGet("/api/products", () => store.GetAllProducts());
        app.MapGet("/api/products/{sku}", (string sku) =>
        {
            var product = store.GetProduct(sku);
            return product is null ? Results.NotFound() : Results.Ok(product);
        });
        app.MapGet("/api/history", () => store.GetHistory());
    }
}