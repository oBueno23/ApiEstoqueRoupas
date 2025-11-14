using Microsoft.EntityFrameworkCore;
using ApiEstoqueRoupas.Models;

namespace ApiEstoqueRoupas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
    }
}