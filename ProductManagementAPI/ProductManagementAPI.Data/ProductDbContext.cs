using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data.Models;
using System.Reflection;

namespace ProductManagementAPI.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ProductDbContext))!);
        }
    }
}
