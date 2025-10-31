using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_GET
{
    public static void MapGetRoutes(this WebApplication app, InventoryStore store)
    {
        // ✅ Listar todos os produtos
        app.MapGet("/api/products", () => store.GetAllProducts());

        // ✅ Buscar produto específico pelo ID
        app.MapGet("/api/products/{id}", (string Id) =>
        {
            var product = store.GetProduct(Id);
            return product is null
                ? Results.NotFound("Produto não encontrado.")
                : Results.Ok(product);
        });

        // ✅ Histórico (opcional)
        app.MapGet("/api/history", () => store.GetHistory());
    }
}
