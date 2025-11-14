using ApiEstoqueRoupas.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueRoupas.Routes
{
    public static class Rota_DELETE
    {
        public static void Map(WebApplication app)
        {
            app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                var p = await db.Products.FindAsync(id);
                if (p is null) return Results.NotFound();

                db.Products.Remove(p);
                await db.SaveChangesAsync();
                return Results.Ok($"Produto {p.Name} removido.");
            });
        }
    }
}
