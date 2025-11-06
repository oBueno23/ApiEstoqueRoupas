using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routes
{
    public static class Rota_DELETE
    {
        public static void MapDeleteRoutes(this WebApplication app, InventoryStore store)
        {
            app.MapDelete("/api/products/{id:int}", (int id) =>
            {
                var product = store.GetProductById(id);
                if (product is null) return Results.NotFound("Produto não encontrado.");

                store.RemoveProduct(id); // método que você implementou
                return Results.Ok($"Produto {product.Name} removido.");
            });
        }
    }
}
