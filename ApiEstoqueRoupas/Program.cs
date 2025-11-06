using ApiEstoqueRoupas.Models;
using ApiEstoqueRoupas.Routes; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var store = new InventoryStore(); 
store.SeedProducts(); 

app.MapGetRoutes(store);
app.MapPostRoutes(store);
app.MapDeleteRoutes(store);

app.Run();
