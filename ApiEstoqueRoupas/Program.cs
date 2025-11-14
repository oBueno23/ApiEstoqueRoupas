using ApiEstoqueRoupas.Data;
using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=estoque.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()  // Permite qualquer origem
            .AllowAnyMethod()  // Permite GET, POST, DELETE, etc.
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configurar arquivos estáticos
app.UseDefaultFiles(); // Procura por index.html, default.html, etc.
app.UseStaticFiles(); 


// Cria o banco e insere dados iniciais, se necessário
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Products.Any())
    {
        var produtosIniciais = new List<Product>
        {
            new Product { Id = 1, Name = "Camisa Polo Azul", Category = "Camisas", Quantity = 30, ReorderThreshold = 5 },
        new Product { Id = 2, Name = "Camisa Branca", Category = "Camisas", Quantity = 25, ReorderThreshold = 5 },
        new Product { Id = 3, Name = "Camisa Preta", Category = "Camisas", Quantity = 40, ReorderThreshold = 8 },
        new Product { Id = 4, Name = "Jaqueta Jeans", Category = "Jaquetas", Quantity = 20, ReorderThreshold = 3 },
        new Product { Id = 5, Name = "Jaqueta de Couro", Category = "Jaquetas", Quantity = 10, ReorderThreshold = 2 },
        new Product { Id = 6, Name = "Calça Jeans Azul", Category = "Calças", Quantity = 35, ReorderThreshold = 6 },
        new Product { Id = 7, Name = "Calça Moletom Cinza", Category = "Calças", Quantity = 28, ReorderThreshold = 4 },
        new Product { Id = 8, Name = "Calça Preta", Category = "Calças", Quantity = 18, ReorderThreshold = 3 },
        new Product { Id = 9, Name = "Meias Brancas (par)", Category = "Meias", Quantity = 100, ReorderThreshold = 20 },
        new Product { Id = 10, Name = "Meias Pretas (par)", Category = "Meias", Quantity = 80, ReorderThreshold = 15 },
        new Product { Id = 11, Name = "Camisa Social Azul", Category = "Camisas", Quantity = 25, ReorderThreshold = 5 },
        new Product { Id = 12, Name = "Camisa Social Branca", Category = "Camisas", Quantity = 30, ReorderThreshold = 6 },
        new Product { Id = 13, Name = "Camisa Estampada", Category = "Camisas", Quantity = 22, ReorderThreshold = 4 },
        new Product { Id = 14, Name = "Jaqueta de Moletom", Category = "Jaquetas", Quantity = 15, ReorderThreshold = 3 },
        new Product { Id = 15, Name = "Jaqueta Puffer", Category = "Jaquetas", Quantity = 12, ReorderThreshold = 2 },
        new Product { Id = 16, Name = "Calça Cargo Verde", Category = "Calças", Quantity = 20, ReorderThreshold = 5 },
        new Product { Id = 17, Name = "Calça Social Preta", Category = "Calças", Quantity = 17, ReorderThreshold = 3 },
        new Product { Id = 18, Name = "Calça Jeans Clara", Category = "Calças", Quantity = 33, ReorderThreshold = 6 },
        new Product { Id = 19, Name = "Meias Coloridas (par)", Category = "Meias", Quantity = 70, ReorderThreshold = 10 },
        new Product { Id = 20, Name = "Meias Esportivas (par)",Category = "Meias", Quantity = 90, ReorderThreshold = 15 },
        new Product { Id = 21, Name = "Camisa Regata Preta", Category = "Camisas", Quantity = 18, ReorderThreshold = 4 },
        new Product { Id = 22, Name = "Camisa Regata Branca", Category = "Camisas", Quantity = 20, ReorderThreshold = 4 },
        new Product { Id = 23, Name = "Camisa Manga Longa", Category = "Camisas", Quantity = 25, ReorderThreshold = 5 },
        new Product { Id = 24, Name = "Jaqueta Esportiva", Category = "Jaquetas", Quantity = 16, ReorderThreshold = 3 },
        new Product { Id = 25, Name = "Jaqueta Corta-Vento", Category = "Jaquetas", Quantity = 14, ReorderThreshold = 3 },
        new Product { Id = 26, Name = "Calça Sarja Bege", Category = "Calças", Quantity = 21, ReorderThreshold = 5 },
        new Product { Id = 27, Name = "Calça Jogger Preta", Category = "Calças", Quantity = 19, ReorderThreshold = 4 },
        new Product { Id = 28, Name = "Calça Legging", Category = "Calças", Quantity = 23, ReorderThreshold = 5 },
        new Product { Id = 29, Name = "Meias Invisíveis (par)", Category = "Meias", Quantity = 75, ReorderThreshold = 12 },
        new Product { Id = 30, Name = "Meias Longas (par)", Category = "Meias", Quantity = 60, ReorderThreshold = 10 },
        new Product { Id = 31, Name = "Camisa Gola V Azul", Category = "Camisas", Quantity = 26, ReorderThreshold = 5 },
        new Product { Id = 32, Name = "Camisa Gola V Preta", Category = "Camisas", Quantity = 28, ReorderThreshold = 6 },
        new Product { Id = 33, Name = "Camisa Gola Careca", Category = "Camisas", Quantity = 30, ReorderThreshold = 5 },
        new Product { Id = 34, Name = "Jaqueta Térmica", Category = "Jaquetas", Quantity = 10, ReorderThreshold = 2 },
        new Product { Id = 35, Name = "Jaqueta Impermeável", Category = "Jaquetas", Quantity = 11, ReorderThreshold = 2 },
        new Product { Id = 36, Name = "Calça Flare", Category = "Calças", Quantity = 15, ReorderThreshold = 4 },
        new Product { Id = 37, Name = "Calça Skinny", Category = "Calças", Quantity = 22, ReorderThreshold = 5 },
        new Product { Id = 38, Name = "Calça Pantacourt", Category = "Calças", Quantity = 13, ReorderThreshold = 3 },
        new Product { Id = 39, Name = "Meias Térmicas (par)", Category = "Meias", Quantity = 50, ReorderThreshold = 8 },
        new Product { Id = 40, Name = "Meias Esportivas Curtas (par)", Category = "Meias", Quantity = 65, ReorderThreshold = 10 },
        new Product { Id = 41, Name = "Camisa Esportiva Dry Fit", Category = "Camisas", Quantity = 24, ReorderThreshold = 5 },
        new Product { Id = 42, Name = "Camisa Gola Alta", Category = "Camisas", Quantity = 20, ReorderThreshold = 4 },
        new Product { Id = 43, Name = "Jaqueta Windbreaker", Category = "Jaquetas", Quantity = 13, ReorderThreshold = 3 },
        new Product { Id = 44, Name = "Jaqueta College", Category = "Jaquetas", Quantity = 14, ReorderThreshold = 3 },
        new Product { Id = 45, Name = "Calça Jeans Rasgada", Category = "Calças", Quantity = 16, ReorderThreshold = 4 },
        new Product { Id = 46, Name = "Calça Jogger Cinza", Category = "Calças", Quantity = 18, ReorderThreshold = 4 },
        new Product { Id = 47, Name = "Calça Social Azul Marinho", Category = "Calças", Quantity = 15, ReorderThreshold = 3 },
        new Product { Id = 48, Name = "Meias Antiderrapantes (par)", Category = "Meias", Quantity = 55, ReorderThreshold = 8 },
        new Product { Id = 49, Name = "Meias Cano Alto (par)", Category = "Meias", Quantity = 70, ReorderThreshold = 10 },
        new Product { Id = 50, Name = "Meias Básicas (par)", Category = "Meias", Quantity = 100, ReorderThreshold = 20 }
        };

        context.Products.AddRange(produtosIniciais);
        context.SaveChanges();
    }
}

// Rotas
app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

app.MapGet("/api/products/{id:int}", async (int id, AppDbContext db) =>
    await db.Products.FindAsync(id) is Product product
        ? Results.Ok(product)
        : Results.NotFound());

app.MapPost("/api/products", async (Product product, AppDbContext db) =>
{
    if (await db.Products.AnyAsync(p => p.Id == product.Id))
        return Results.BadRequest("Já existe um produto com esse ID.");

    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapDelete("/api/products/{id:int}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();