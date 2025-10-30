namespace ApiEstoqueRoupas.Models;

public class Product
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ReorderThreshold { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}