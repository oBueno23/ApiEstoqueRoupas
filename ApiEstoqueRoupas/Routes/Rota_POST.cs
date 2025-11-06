using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routes
{
    public static class Rota_POST
    {
        public static void MapPostRoutes(this WebApplication app, InventoryStore store)
        {
            app.MapPost("/api/products", (Product product) =>
            {
                if (product.Id == 0)
                    return Results.BadRequest("O ID do produto deve ser informado e diferente de 0.");

                if (string.IsNullOrWhiteSpace(product.Name))
                    return Results.BadRequest("O nome do produto é obrigatório.");

                if (store.GetProductById(product.Id) != null)
                    return Results.Conflict($"Já existe produto com Id {product.Id}.");

                store.AddProduct(product);
                return Results.Created($"/api/products/{product.Id}", product);
            });
        }
    }
}

