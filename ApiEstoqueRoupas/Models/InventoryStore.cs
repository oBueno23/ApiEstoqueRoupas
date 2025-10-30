using System.Collections.Concurrent;

namespace ApiEstoqueRoupas.Models;

public class InventoryStore
{
    private readonly ConcurrentDictionary<string, Product> _products = new();
    private readonly ConcurrentBag<string> _history = new();

    public IEnumerable<Product> GetAllProducts() => _products.Values;
    public Product? GetProduct(string sku) => _products.TryGetValue(sku, out var p) ? p : null;
    public IEnumerable<string> GetHistory() => _history;

    public void AddProduct(Product product)
    {
        if (_products.ContainsKey(product.Sku))
            throw new Exception("Produto já existe.");

        _products[product.Sku] = product;
        _history.Add($"[{DateTime.UtcNow}] Produto criado: {product.Sku} - {product.Name}");
    }

    public void RegisterEntry(string sku, int qty)
    {
        if (!_products.ContainsKey(sku))
            throw new Exception("Produto não encontrado.");

        _products[sku].Quantity += qty;
        _history.Add($"[{DateTime.UtcNow}] Entrada: +{qty} unidades de {sku}");
    }

    public void RegisterExit(string sku, int qty)
    {
        if (!_products.ContainsKey(sku))
            throw new Exception("Produto não encontrado.");

        var product = _products[sku];
        if (product.Quantity < qty)
            throw new Exception("Estoque insuficiente.");

        product.Quantity -= qty;
        _history.Add($"[{DateTime.UtcNow}] Saída: -{qty} unidades de {sku}");

        if (product.Quantity <= product.ReorderThreshold)
            RegisterReorder(sku, 10);
    }

    public void RegisterReorder(string sku, int qty)
    {
        if (!_products.ContainsKey(sku))
            throw new Exception("Produto não encontrado.");

        _products[sku].Quantity += qty;
        _history.Add($"[{DateTime.UtcNow}] Reposição automática de {qty} unidades de {sku}");
    }

    public void DeleteProduct(string sku)
    {
        if (_products.TryRemove(sku, out _))
            _history.Add($"[{DateTime.UtcNow}] Produto removido: {sku}");
    }

    public void Seed()
    {
        AddProduct(new Product
        {
            Sku = "CAM-001",
            Name = "Camiseta Branca",
            Quantity = 20,
            ReorderThreshold = 5
        });
    }
}