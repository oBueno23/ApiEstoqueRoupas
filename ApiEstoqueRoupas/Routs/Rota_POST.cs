using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Routs;

public static class Rota_POST
{
    public static void MapPostRoutes(this WebApplication app, InventoryStore store)
    {
        app.MapPost("/api/actions", (ActionRequest request) =>
        {
            try
            {
                switch (request.Action.ToLower())
                {
                    case "create_product":
                        store.AddProduct(new Product
                        {
                            id = request.id ?? Guid.NewGuid().ToString(),
                            Name = request.Name ?? "Produto sem nome",
                            Quantity = request.Quantity,
                            ReorderThreshold = request.ReorderThreshold
                        });
                        break;

                    case "entry":
                        store.RegisterEntry(request.id!, request.Quantity);
                        break;

                    case "exit":
                        store.RegisterExit(request.id!, request.Quantity);
                        break;

                    case "reorder":
                        store.RegisterReorder(request.id!, request.Quantity);
                        break;

                    default:
                        return Results.BadRequest("Ação inválida.");
                }

                return Results.Ok("Ação executada com sucesso.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}