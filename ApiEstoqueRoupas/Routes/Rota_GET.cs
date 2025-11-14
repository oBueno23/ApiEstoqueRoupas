using ApiEstoqueRoupas.Data;
using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueRoupas.Routes
{
    public static class Rota_GET
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/produtos", async (AppDbContext db) =>
                await db.Products.AsNoTracking().ToListAsync());

            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                var p = await db.Products.FindAsync(id);
                return p is not null ? Results.Ok(p) : Results.NotFound();
            });
        }
    }
}
