namespace ApiEstoqueRoupas.Models;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; } // Quantidade atual em estoque
    public int InitialStock { get; set; } 
    public int ReorderThreshold { get; set; } // Nível mínimo antes da reposição
}
