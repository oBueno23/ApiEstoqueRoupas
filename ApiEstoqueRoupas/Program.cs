using ApiEstoqueRoupas.Models;
using ApiEstoqueRoupas.Routes; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=estoque.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 

    // Seed: insere 50 produtos se não houver nenhum
    if (!db.Products.Any())
    {
        var produtos = GetInitialProducts(); 
        db.Products.AddRange(produtos);
        db.SaveChanges();
    }
}

app.MapGetRoutes(); 
app.MapPostRoutes();
app.MapDeleteRoutes();

app.Run();


// Método local para gerar os 50 produtos (Id manual)
static List<Product> GetInitialProducts()
{
    return new List<Product>
    {
        new Product { Id = 1, Name = "Camisa Polo Azul", Quantity = 30, ReorderThreshold = 5 },
        new Product { Id = 2, Name = "Camisa Branca", Quantity = 25, ReorderThreshold = 5 },
        new Product { Id = 3, Name = "Camisa Preta", Quantity = 40, ReorderThreshold = 8 },
        new Product { Id = 4, Name = "Jaqueta Jeans", Quantity = 20, ReorderThreshold = 3 },
        new Product { Id = 5, Name = "Jaqueta de Couro", Quantity = 10, ReorderThreshold = 2 },
        new Product { Id = 6, Name = "Calça Jeans Azul", Quantity = 35, ReorderThreshold = 6 },
        new Product { Id = 7, Name = "Calça Moletom Cinza", Quantity = 28, ReorderThreshold = 4 },
        new Product { Id = 8, Name = "Calça Preta", Quantity = 18, ReorderThreshold = 3 },
        new Product { Id = 9, Name = "Meias Brancas (par)", Quantity = 100, ReorderThreshold = 20 },
        new Product { Id = 10, Name = "Meias Pretas (par)", Quantity = 80, ReorderThreshold = 15 },
        new Product { Id = 11, Name = "Camisa Social Azul", Quantity = 25, ReorderThreshold = 5 },
        new Product { Id = 12, Name = "Camisa Social Branca", Quantity = 30, ReorderThreshold = 6 },
        new Product { Id = 13, Name = "Camisa Estampada", Quantity = 22, ReorderThreshold = 4 },
        new Product { Id = 14, Name = "Jaqueta de Moletom", Quantity = 15, ReorderThreshold = 3 },
        new Product { Id = 15, Name = "Jaqueta Puffer", Quantity = 12, ReorderThreshold = 2 },
        new Product { Id = 16, Name = "Calça Cargo Verde", Quantity = 20, ReorderThreshold = 5 },
        new Product { Id = 17, Name = "Calça Social Preta", Quantity = 17, ReorderThreshold = 3 },
        new Product { Id = 18, Name = "Calça Jeans Clara", Quantity = 33, ReorderThreshold = 6 },
        new Product { Id = 19, Name = "Meias Coloridas (par)", Quantity = 70, ReorderThreshold = 10 },
        new Product { Id = 20, Name = "Meias Esportivas (par)", Quantity = 90, ReorderThreshold = 15 },
        new Product { Id = 21, Name = "Camisa Regata Preta", Quantity = 18, ReorderThreshold = 4 },
        new Product { Id = 22, Name = "Camisa Regata Branca", Quantity = 20, ReorderThreshold = 4 },
        new Product { Id = 23, Name = "Camisa Manga Longa", Quantity = 25, ReorderThreshold = 5 },
        new Product { Id = 24, Name = "Jaqueta Esportiva", Quantity = 16, ReorderThreshold = 3 },
        new Product { Id = 25, Name = "Jaqueta Corta-Vento", Quantity = 14, ReorderThreshold = 3 },
        new Product { Id = 26, Name = "Calça Sarja Bege", Quantity = 21, ReorderThreshold = 5 },
        new Product { Id = 27, Name = "Calça Jogger Preta", Quantity = 19, ReorderThreshold = 4 },
        new Product { Id = 28, Name = "Calça Legging", Quantity = 23, ReorderThreshold = 5 },
        new Product { Id = 29, Name = "Meias Invisíveis (par)", Quantity = 75, ReorderThreshold = 12 },
        new Product { Id = 30, Name = "Meias Longas (par)", Quantity = 60, ReorderThreshold = 10 },
        new Product { Id = 31, Name = "Camisa Gola V Azul", Quantity = 26, ReorderThreshold = 5 },
        new Product { Id = 32, Name = "Camisa Gola V Preta", Quantity = 28, ReorderThreshold = 6 },
        new Product { Id = 33, Name = "Camisa Gola Careca", Quantity = 30, ReorderThreshold = 5 },
        new Product { Id = 34, Name = "Jaqueta Térmica", Quantity = 10, ReorderThreshold = 2 },
        new Product { Id = 35, Name = "Jaqueta Impermeável", Quantity = 11, ReorderThreshold = 2 },
        new Product { Id = 36, Name = "Calça Flare", Quantity = 15, ReorderThreshold = 4 },
        new Product { Id = 37, Name = "Calça Skinny", Quantity = 22, ReorderThreshold = 5 },
        new Product { Id = 38, Name = "Calça Pantacourt", Quantity = 13, ReorderThreshold = 3 },
        new Product { Id = 39, Name = "Meias Térmicas (par)", Quantity = 50, ReorderThreshold = 8 },
        new Product { Id = 40, Name = "Meias Esportivas Curtas (par)", Quantity = 65, ReorderThreshold = 10 },
        new Product { Id = 41, Name = "Camisa Esportiva Dry Fit", Quantity = 24, ReorderThreshold = 5 },
        new Product { Id = 42, Name = "Camisa Gola Alta", Quantity = 20, ReorderThreshold = 4 },
        new Product { Id = 43, Name = "Jaqueta Windbreaker", Quantity = 13, ReorderThreshold = 3 },
        new Product { Id = 44, Name = "Jaqueta College", Quantity = 14, ReorderThreshold = 3 },
        new Product { Id = 45, Name = "Calça Jeans Rasgada", Quantity = 16, ReorderThreshold = 4 },
        new Product { Id = 46, Name = "Calça Jogger Cinza", Quantity = 18, ReorderThreshold = 4 },
        new Product { Id = 47, Name = "Calça Social Azul Marinho", Quantity = 15, ReorderThreshold = 3 },
        new Product { Id = 48, Name = "Meias Antiderrapantes (par)", Quantity = 55, ReorderThreshold = 8 },
        new Product { Id = 49, Name = "Meias Cano Alto (par)", Quantity = 70, ReorderThreshold = 10 },
        new Product { Id = 50, Name = "Meias Básicas (par)", Quantity = 100, ReorderThreshold = 20 }
    };
}

