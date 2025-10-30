using ApiEstoqueRoupas.Models;
using ApiEstoqueRoupas.Routs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var store = new InventoryStore();
store.Seed(); // Dados iniciais

app.MapGetRoutes(store);
app.MapPostRoutes(store);
app.MapDeleteRoutes(store);

app.Run();