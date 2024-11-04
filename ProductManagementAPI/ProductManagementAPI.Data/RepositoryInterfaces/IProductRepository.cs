﻿using ProductManagementAPI.Data.Models;

namespace ProductManagementAPI.Data.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product?> GetProductByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}