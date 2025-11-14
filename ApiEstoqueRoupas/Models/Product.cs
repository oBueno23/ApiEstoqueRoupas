namespace ApiEstoqueRoupas.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ReorderThreshold { get; set; }
    }
    
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int StockBefore { get; set; }
        public int StockAfter { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string User { get; set; } = "Sistema";
        public Product? Product { get; set; }
    }
    
    public class StockEntryRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; } = "Reposição";
        public string User { get; set; } = "Sistema";
    }
    
    public class StockExitRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; } = "Venda";
        public string User { get; set; } = "Sistema";
    }
    
    public class StockMovementResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public StockMovement? Movement { get; set; }
        public bool NeedsRestock { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderThreshold { get; set; }
    }
    
    public class RestockAlert
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int ReorderThreshold { get; set; }
        public int SuggestedOrderQuantity { get; set; }
        public string AlertLevel { get; set; } = string.Empty;
    }
}