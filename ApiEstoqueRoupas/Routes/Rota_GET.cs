using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routes
{
    public static class Rota_GET
    {
        public static void MapGetRoutes(this WebApplication app, InventoryStore store)
        {
            app.MapGet("/api/products", () => store.GetAllProducts());

            app.MapGet("/api/products/{id:int}", (int id) =>
            {
                var product = store.GetProductById(id);
                return product is not null ? Results.Ok(product) : Results.NotFound("Produto não encontrado.");
            });
        }
    }
}