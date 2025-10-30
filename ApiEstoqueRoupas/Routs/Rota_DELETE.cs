using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app, InventoryStore store)
    {
        app.MapDelete("/api/products/{sku}", (string sku) =>
        {
            store.DeleteProduct(sku);
            return Results.Ok($"Produto {sku} removido.");
        });
    }
}