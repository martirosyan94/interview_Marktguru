using ProductManagementAPI.Data.Models;

namespace ProductManagementAPI.Data.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product?> GetProductByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Product?>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
