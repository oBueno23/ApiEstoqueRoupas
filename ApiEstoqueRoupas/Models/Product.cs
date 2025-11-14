namespace ApiEstoqueRoupas.Models
{
    public class Product
    {
        public int Id { get; set; }                         // ID manual (não gerado automaticamente)
        public string Name { get; set; } = string.Empty;     // Nome do produto
        public string Category { get; set; } = string.Empty; // Categoria (ex: Camiseta, Calça, etc.)
        public int Quantity { get; set; }                    // Quantidade em estoque
        public int ReorderThreshold { get; set; }            // Quantidade mínima antes de repor
    }
}
