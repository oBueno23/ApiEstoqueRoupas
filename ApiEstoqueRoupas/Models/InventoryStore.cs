using System.Collections.Concurrent;

namespace ApiEstoqueRoupas.Models;

public class InventoryStore
{
    private readonly ConcurrentDictionary<string, Product> _products = new();
    private readonly ConcurrentBag<string> _history = new();

    public IEnumerable<Product> GetAllProducts() => _products.Values;
    public Product? GetProduct(string id) => _products.TryGetValue(id, out var p) ? p : null;
    public IEnumerable<string> GetHistory() => _history;

    public void AddProduct(Product product)
    {
        if (_products.ContainsKey(product.id))
            throw new Exception("Produto já existe.");

        _products[product.id] = product;
        _history.Add($"[{DateTime.UtcNow}] Produto criado: {product.id} - {product.Name}");
    }

    public void RegisterEntry(string id, int qty)
    {
        if (!_products.ContainsKey(id))
            throw new Exception("Produto não encontrado.");

        _products[id].Quantity += qty;
        _history.Add($"[{DateTime.UtcNow}] Entrada: +{qty} unidades de {id}");
    }

    public void RegisterExit(string id, int qty)
    {
        if (!_products.ContainsKey(id))
            throw new Exception("Produto não encontrado.");

        var product = _products[id];
        if (product.Quantity < qty)
            throw new Exception("Estoque insuficiente.");

        product.Quantity -= qty;
        _history.Add($"[{DateTime.UtcNow}] Saída: -{qty} unidades de {id}");

        if (product.Quantity <= product.ReorderThreshold)
            RegisterReorder(id, 10);
    }

    public void RegisterReorder(string id, int qty)
    {
        if (!_products.ContainsKey(id))
            throw new Exception("Produto não encontrado.");

        _products[id].Quantity += qty;
        _history.Add($"[{DateTime.UtcNow}] Reposição automática de {qty} unidades de {id}");
    }

    public void DeleteProduct(string id)
    {
        if (_products.TryRemove(id, out _))
            _history.Add($"[{DateTime.UtcNow}] Produto removido: {id}");
    }

    public void Seed()
    {
        AddProduct(new Product
        {
            id = "CAM-001",
            Name = "Camiseta Branca",
            Quantity = 20,
            ReorderThreshold = 5
        });
    }
}