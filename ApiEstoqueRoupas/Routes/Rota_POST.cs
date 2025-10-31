using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_POST
{
    public static void MapPostRoutes(this WebApplication app, InventoryStore store)
    {
        
        app.MapPost("/api/products", (Product product) =>
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return Results.BadRequest("O nome do produto é obrigatório.");

            product.Id = Guid.NewGuid().ToString();
            product.InitialStock = product.Quantity; // Define estoque inicial
            store.AddProduct(product);

            return Results.Created($"/api/products/{product.Id}", product);
        });
    }
}
