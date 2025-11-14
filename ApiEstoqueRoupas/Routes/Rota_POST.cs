using ApiEstoqueRoupas.Data;
using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueRoupas.Routes
{
    public static class Rota_POST
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/produtos", async (Product novoProduto, AppDbContext db) =>
            {
                if (novoProduto.Id == 0) return Results.BadRequest("Informe um Id inteiro diferente de 0.");
                if (string.IsNullOrWhiteSpace(novoProduto.Name)) return Results.BadRequest("Nome obrigatório.");

                if (await db.Products.AnyAsync(x => x.Id == novoProduto.Id))
                    return Results.Conflict($"Produto com ID {novoProduto.Id} já existe.");

                db.Products.Add(novoProduto);
                await db.SaveChangesAsync();
                return Results.Created($"/produtos/{novoProduto.Id}", novoProduto);
            });
        }
    }
}
