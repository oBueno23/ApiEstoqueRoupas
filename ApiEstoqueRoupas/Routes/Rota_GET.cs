using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueRoupas.Routes;

public static class Rota_GET
{
    public static void MapGetRoutes(this WebApplication app)
    {
        app.MapGet("/api/products", async (AppDbContext db) =>
            await db.Products.AsNoTracking().ToListAsync());

        app.MapGet("/api/products/{id:int}", async (int id, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });
    }
}