using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app, InventoryStore store)
    {
        app.MapDelete("/api/products/{id}", (string id) =>
        {
            store.DeleteProduct(id);
            return Results.Ok($"Produto {id} removido.");
        });
    }
}