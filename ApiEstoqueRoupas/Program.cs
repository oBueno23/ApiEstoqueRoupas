using ApiEstoqueRoupas.Models;
using ApiEstoqueRoupas.Routs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();



// ✅ Instancia o "banco" em memória e gera 50 produtos iniciais
var store = new InventoryStore();
store.Seed();

// ✅ Mapeia as rotas do CRUD
app.MapGetRoutes(store);
app.MapPostRoutes(store);
app.MapDeleteRoutes(store);

app.Run();

