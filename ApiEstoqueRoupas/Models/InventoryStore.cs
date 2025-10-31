namespace ApiEstoqueRoupas.Models;

public class InventoryStore
{
    private List<Product> products = new();
    private List<string> history = new();

    // ✅ Gera 50 registros iniciais com IDs manuais
    public void Seed()
    {
        var produtosIniciais = new List<Product>
        {
            // 👕 Camisas (P001–P015)
            new Product { Id="P001", Name="Camisa Polo Azul", Quantity=25, InitialStock=25, ReorderThreshold=5 },
            new Product { Id="P002", Name="Camisa Social Branca", Quantity=40, InitialStock=40, ReorderThreshold=8 },
            new Product { Id="P003", Name="Camisa Estampada", Quantity=30, InitialStock=30, ReorderThreshold=6 },
            new Product { Id="P004", Name="Camisa Xadrez Vermelha", Quantity=18, InitialStock=18, ReorderThreshold=4 },
            new Product { Id="P005", Name="Camisa Preta Básica", Quantity=50, InitialStock=50, ReorderThreshold=10 },
            new Product { Id="P006", Name="Camisa Longline Cinza", Quantity=22, InitialStock=22, ReorderThreshold=5 },
            new Product { Id="P007", Name="Camisa Regata Branca", Quantity=15, InitialStock=15, ReorderThreshold=3 },
            new Product { Id="P008", Name="Camisa Gola V Azul-Marinho", Quantity=27, InitialStock=27, ReorderThreshold=5 },
            new Product { Id="P009", Name="Camisa Manga Longa Verde", Quantity=35, InitialStock=35, ReorderThreshold=7 },
            new Product { Id="P010", Name="Camisa Básica Vermelha", Quantity=20, InitialStock=20, ReorderThreshold=4 },
            new Product { Id="P011", Name="Camisa Branca Oversized", Quantity=28, InitialStock=28, ReorderThreshold=6 },
            new Product { Id="P012", Name="Camisa Gola Alta Preta", Quantity=17, InitialStock=17, ReorderThreshold=3 },
            new Product { Id="P013", Name="Camisa Estilo Retrô", Quantity=19, InitialStock=19, ReorderThreshold=4 },
            new Product { Id="P014", Name="Camisa Amarela Casual", Quantity=23, InitialStock=23, ReorderThreshold=5 },
            new Product { Id="P015", Name="Camisa Azul Clara Slim", Quantity=25, InitialStock=25, ReorderThreshold=5 },

            // 🧥 Jaquetas (P016–P025)
            new Product { Id="P016", Name="Jaqueta Jeans Azul", Quantity=18, InitialStock=18, ReorderThreshold=4 },
            new Product { Id="P017", Name="Jaqueta Couro Preta", Quantity=10, InitialStock=10, ReorderThreshold=2 },
            new Product { Id="P018", Name="Jaqueta Corta-Vento Cinza", Quantity=12, InitialStock=12, ReorderThreshold=3 },
            new Product { Id="P019", Name="Jaqueta Puffer Vermelha", Quantity=8, InitialStock=8, ReorderThreshold=2 },
            new Product { Id="P020", Name="Jaqueta Moletom Azul", Quantity=20, InitialStock=20, ReorderThreshold=4 },
            new Product { Id="P021", Name="Jaqueta Militar Verde", Quantity=15, InitialStock=15, ReorderThreshold=3 },
            new Product { Id="P022", Name="Jaqueta Nylon Preta", Quantity=13, InitialStock=13, ReorderThreshold=3 },
            new Product { Id="P023", Name="Jaqueta Jeans Clara", Quantity=11, InitialStock=11, ReorderThreshold=2 },
            new Product { Id="P024", Name="Jaqueta Couro Marrom", Quantity=9, InitialStock=9, ReorderThreshold=2 },
            new Product { Id="P025", Name="Jaqueta Bomber Preta", Quantity=14, InitialStock=14, ReorderThreshold=3 },

            // 👖 Calças (P026–P040)
            new Product { Id="P026", Name="Calça Jeans Azul", Quantity=35, InitialStock=35, ReorderThreshold=6 },
            new Product { Id="P027", Name="Calça Jeans Preta", Quantity=28, InitialStock=28, ReorderThreshold=5 },
            new Product { Id="P028", Name="Calça Moletom Cinza", Quantity=40, InitialStock=40, ReorderThreshold=8 },
            new Product { Id="P029", Name="Calça Sarja Bege", Quantity=22, InitialStock=22, ReorderThreshold=4 },
            new Product { Id="P030", Name="Calça Cargo Verde", Quantity=19, InitialStock=19, ReorderThreshold=4 },
            new Product { Id="P031", Name="Calça Social Preta", Quantity=30, InitialStock=30, ReorderThreshold=6 },
            new Product { Id="P032", Name="Calça Jeans Destroyed", Quantity=17, InitialStock=17, ReorderThreshold=3 },
            new Product { Id="P033", Name="Calça Jogger Preta", Quantity=26, InitialStock=26, ReorderThreshold=5 },
            new Product { Id="P034", Name="Calça de Linho Bege", Quantity=15, InitialStock=15, ReorderThreshold=3 },
            new Product { Id="P035", Name="Calça Tática Militar", Quantity=12, InitialStock=12, ReorderThreshold=2 },
            new Product { Id="P036", Name="Calça Jeans Slim Azul Claro", Quantity=33, InitialStock=33, ReorderThreshold=6 },
            new Product { Id="P037", Name="Calça Moletom Preta", Quantity=25, InitialStock=25, ReorderThreshold=5 },
            new Product { Id="P038", Name="Calça de Sarja Cinza", Quantity=21, InitialStock=21, ReorderThreshold=4 },
            new Product { Id="P039", Name="Calça Jeans Tradicional", Quantity=29, InitialStock=29, ReorderThreshold=5 },
            new Product { Id="P040", Name="Calça Jeans Reta Azul Escuro", Quantity=32, InitialStock=32, ReorderThreshold=6 },

            // 🧦 Meias (P041–P050)
            new Product { Id="P041", Name="Meia Branca Cano Baixo", Quantity=60, InitialStock=60, ReorderThreshold=10 },
            new Product { Id="P042", Name="Meia Preta Esportiva", Quantity=55, InitialStock=55, ReorderThreshold=10 },
            new Product { Id="P043", Name="Meia Colorida Divertida", Quantity=40, InitialStock=40, ReorderThreshold=8 },
            new Product { Id="P044", Name="Meia Social Preta", Quantity=70, InitialStock=70, ReorderThreshold=12 },
            new Product { Id="P045", Name="Meia Cinza Algodão", Quantity=45, InitialStock=45, ReorderThreshold=8 },
            new Product { Id="P046", Name="Meia Azul Marinho", Quantity=38, InitialStock=38, ReorderThreshold=7 },
            new Product { Id="P047", Name="Meia Estampada Geek", Quantity=50, InitialStock=50, ReorderThreshold=10 },
            new Product { Id="P048", Name="Meia Branca Esportiva", Quantity=65, InitialStock=65, ReorderThreshold=12 },
            new Product { Id="P049", Name="Meia Infantil Colorida", Quantity=48, InitialStock=48, ReorderThreshold=9 },
            new Product { Id="P050", Name="Meia Térmica Inverno", Quantity=30, InitialStock=30, ReorderThreshold=6 }
        };

        products.AddRange(produtosIniciais);
    }

    public List<Product> GetAllProducts() => products;

    public Product? GetProduct(string id) =>
        products.FirstOrDefault(p => p.Id == id);

    public void AddProduct(Product product)
    {
        // Define o estoque inicial igual à quantidade
        product.InitialStock = product.Quantity;
        products.Add(product);
        history.Add($"[CREATE] Produto '{product.Name}' (ID: {product.Id}) adicionado ({product.Quantity} unidades).");
    }

    public void DeleteProduct(string id)
    {
        var product = GetProduct(id);
        if (product != null)
        {
            products.Remove(product);
            history.Add($"[DELETE] Produto '{product.Name}' (ID: {product.Id}) removido.");
        }
    }

    public List<string> GetHistory() => history;
}
