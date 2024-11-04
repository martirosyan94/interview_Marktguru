using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data.Models;
using ProductManagementAPI.Data.RepositoryInterfaces;

namespace ProductManagementAPI.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken) =>
            await _context.Products.AddAsync(product, cancellationToken);

        public async Task<Product?> GetProductByNameAsync(string name, CancellationToken cancellationToken) =>
            await _context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) =>
            (await _context.SaveChangesAsync(cancellationToken)) > 0;

        public async Task<IEnumerable<Product?>> GetAllProductsAsync(CancellationToken cancellationToken) =>
            await _context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    }
}
