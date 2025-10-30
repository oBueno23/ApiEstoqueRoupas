namespace ApiEstoqueRoupas.Models;

public class ActionRequest
{
    public string Action { get; set; } = string.Empty; // create_product, entry, exit, reorder
    public string? Sku { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public int ReorderThreshold { get; set; }
}