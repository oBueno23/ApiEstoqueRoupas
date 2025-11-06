using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEstoqueRoupas.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] 
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int ReorderThreshold { get; set; }
}