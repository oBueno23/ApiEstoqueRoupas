using ApiEstoqueRoupas.Data;
using ApiEstoqueRoupas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=estoque.db"));

// Configura CORS - IMPORTANTE: Deve estar aqui
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configurar arquivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();



// Cria o banco e insere dados iniciais, se necessário
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    context.Database.EnsureDeleted(); // Remove apenas na primeira vez
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
            new Product { Id = 20, Name = "Meias Esportivas (par)", Category = "Meias", Quantity = 90, ReorderThreshold = 15 },
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
        
        Console.WriteLine($"✓ Banco de dados criado com {produtosIniciais.Count} produtos iniciais!");
    }
    else
    {
        Console.WriteLine($"✓ Banco de dados já existe com {context.Products.Count()} produtos!");
    }
}

// ROTAS DA API
Console.WriteLine("\n Configurando rotas da API...\n");

// ROTAS DE MOVIMENTAÇÃO DE ESTOQUE 

app.MapPost("/api/stock/entry", async (StockEntryRequest request, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(request.ProductId);
    
    if (product is null)
        return Results.NotFound(new { message = "Produto não encontrado" });
    
    if (request.Quantity <= 0)
        return Results.BadRequest(new { message = "Quantidade deve ser maior que zero" });
    
    var movement = new StockMovement
    {
        ProductId = product.Id,
        ProductName = product.Name,
        Type = "ENTRADA",
        Quantity = request.Quantity,
        StockBefore = product.Quantity,
        StockAfter = product.Quantity + request.Quantity,
        Reason = request.Reason,
        User = request.User,
        Date = DateTime.Now
    };
    
    product.Quantity += request.Quantity;
    
    db.StockMovements.Add(movement);
    await db.SaveChangesAsync();
    
    Console.WriteLine($"ENTRADA: {request.Quantity}x '{product.Name}' | {movement.StockBefore} → {movement.StockAfter}");
    
    return Results.Ok(new StockMovementResponse
    {
        Success = true,
        Message = $"Entrada registrada! Novo estoque: {product.Quantity}",
        Movement = movement,
        NeedsRestock = product.Quantity <= product.ReorderThreshold,
        CurrentStock = product.Quantity,
        ReorderThreshold = product.ReorderThreshold
    });
});

// POST: Saída de estoque
app.MapPost("/api/stock/exit", async (StockExitRequest request, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(request.ProductId);
    
    if (product is null)
        return Results.NotFound(new { message = "Produto não encontrado" });
    
    if (request.Quantity <= 0)
        return Results.BadRequest(new { message = "Quantidade deve ser maior que zero" });
    
    if (product.Quantity < request.Quantity)
        return Results.BadRequest(new { message = $"Estoque insuficiente! Disponível: {product.Quantity}" });
    
    var movement = new StockMovement
    {
        ProductId = product.Id,
        ProductName = product.Name,
        Type = "SAIDA",
        Quantity = request.Quantity,
        StockBefore = product.Quantity,
        StockAfter = product.Quantity - request.Quantity,
        Reason = request.Reason,
        User = request.User,
        Date = DateTime.Now
    };
    
    product.Quantity -= request.Quantity;
    
    db.StockMovements.Add(movement);
    await db.SaveChangesAsync();
    
    var needsRestock = product.Quantity <= product.ReorderThreshold;
    var alertSymbol = needsRestock ? "⚠️" : "✅";
    
    Console.WriteLine($"{alertSymbol} SAÍDA: {request.Quantity}x '{product.Name}' | {movement.StockBefore} → {movement.StockAfter}");
    
    if (needsRestock)
        Console.WriteLine($"ALERTA: Estoque baixo para '{product.Name}'!");
    
    return Results.Ok(new StockMovementResponse
    {
        Success = true,
        Message = needsRestock 
            ? $"ATENÇÃO: Estoque baixo! Reabastecer. Atual: {product.Quantity}"
            : $"Saída registrada! Estoque: {product.Quantity}",
        Movement = movement,
        NeedsRestock = needsRestock,
        CurrentStock = product.Quantity,
        ReorderThreshold = product.ReorderThreshold
    });
});

// GET: Histórico de um produto
app.MapGet("/api/stock/history/{productId:int}", async (int productId, AppDbContext db) =>
{
    var movements = await db.StockMovements
        .Where(m => m.ProductId == productId)
        .OrderByDescending(m => m.Date)
        .ToListAsync();
    
    return Results.Ok(movements);
});

// GET: Todas as movimentações
app.MapGet("/api/stock/movements", async (string? type, AppDbContext db) =>
{
    var query = db.StockMovements.AsQueryable();
    
    if (!string.IsNullOrEmpty(type))
        query = query.Where(m => m.Type == type.ToUpper());
    
    var movements = await query
        .OrderByDescending(m => m.Date)
        .Take(100)
        .ToListAsync();
    
    return Results.Ok(movements);
});

// GET: Alertas de reposição
app.MapGet("/api/stock/restock-alerts", async (AppDbContext db) =>
{
    var products = await db.Products
        .Where(p => p.Quantity <= p.ReorderThreshold)
        .ToListAsync();
    
    var alerts = products.Select(p => new RestockAlert
    {
        ProductId = p.Id,
        ProductName = p.Name,
        Category = p.Category,
        CurrentStock = p.Quantity,
        ReorderThreshold = p.ReorderThreshold,
        SuggestedOrderQuantity = (p.ReorderThreshold * 3) - p.Quantity,
        AlertLevel = p.Quantity == 0 ? "CRITICAL" : "WARNING"
    }).OrderBy(a => a.CurrentStock).ToList();
    
    return Results.Ok(new { count = alerts.Count, alerts });
});

// GET: Relatório
app.MapGet("/api/stock/report", async (AppDbContext db) =>
{
    var totalProducts = await db.Products.CountAsync();
    var lowStockCount = await db.Products.CountAsync(p => p.Quantity <= p.ReorderThreshold);
    var outOfStockCount = await db.Products.CountAsync(p => p.Quantity == 0);
    var totalStockValue = await db.Products.SumAsync(p => p.Quantity);
    
    var todayMovements = await db.StockMovements
        .Where(m => m.Date.Date == DateTime.Today)
        .ToListAsync();
    
    var todayEntries = todayMovements.Where(m => m.Type == "ENTRADA").Sum(m => m.Quantity);
    var todayExits = todayMovements.Where(m => m.Type == "SAIDA").Sum(m => m.Quantity);
    
    return Results.Ok(new
    {
        totalProducts,
        lowStockCount,
        outOfStockCount,
        totalStockValue,
        todayEntries,
        todayExits,
        lastUpdate = DateTime.Now
    });
});

// ROTAS DE PRODUTOS 

app.MapGet("/api/products", async (AppDbContext db) =>
{
    var products = await db.Products.ToListAsync();
    return Results.Ok(products);
});

app.MapGet("/api/products/{id:int}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

app.MapPost("/api/products", async (Product product, AppDbContext db) =>
{
    if (await db.Products.AnyAsync(p => p.Id == product.Id))
        return Results.BadRequest("ID já existe");

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
    return Results.Ok(new { message = "Produto deletado" });
});

Console.WriteLine("\n SERVIDOR RODANDO!");
Console.WriteLine($"   Frontend: http://localhost:5123");
Console.WriteLine($"   API: http://localhost:5123/api/products");
Console.WriteLine($"   Banco: estoque.db\n");

app.Run();