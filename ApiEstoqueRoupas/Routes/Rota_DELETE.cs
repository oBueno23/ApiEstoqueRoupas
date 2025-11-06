using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueRoupas.Routes;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app)
    {
        app.MapDelete("/api/products/{id:int}", async (int id, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Results.Ok($"Produto {product.Name} removido.");
        });
    }
}


