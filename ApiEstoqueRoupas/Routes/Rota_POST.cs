using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueRoupas.Routes;

public static class Rota_POST
{
    public static void MapPostRoutes(this WebApplication app)
    {
        app.MapPost("/api/products", async (Product product, AppDbContext db) =>
        {
            if (product.Id == 0) return Results.BadRequest("Informe um Id inteiro diferente de 0.");
            if (string.IsNullOrWhiteSpace(product.Name)) return Results.BadRequest("Nome obrigatório.");

            var exists = await db.Products.AnyAsync(p => p.Id == product.Id);
            if (exists) return Results.Conflict($"Produto com Id {product.Id} já existe.");

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Results.Created($"/api/products/{product.Id}", product);
        });
    }
}



