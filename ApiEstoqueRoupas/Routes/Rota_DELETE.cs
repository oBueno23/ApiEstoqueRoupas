using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app, InventoryStore store)
    {
        // ✅ Excluir produto por ID
        app.MapDelete("/api/products/{id}", (string Id) =>
        {
            var product = store.GetProduct(Id);
            if (product is null)
                return Results.NotFound("Produto não encontrado.");

            store.DeleteProduct(Id);
            return Results.Ok($"Produto '{product.Name}' removido com sucesso.");
        });
    }
}
