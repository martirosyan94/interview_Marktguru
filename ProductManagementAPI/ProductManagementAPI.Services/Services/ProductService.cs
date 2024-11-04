﻿using ProductManagementAPI.Data;
using ProductManagementAPI.Data.RepositoryInterfaces;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Utilities;

namespace ProductManagementAPI.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult<PostProductDto>> AddProductAsync(PostProductDto product, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductByNameAsync(product.Name, cancellationToken);
            if (existingProduct != null)
                return OperationResult<PostProductDto>.Error("This product already exists", Status.BadRequest);

            await _productRepository.AddProductAsync(product.ToProductEntityModel(), cancellationToken);
            var success = await _productRepository.SaveChangesAsync(cancellationToken);

            if (!success)
                return OperationResult<PostProductDto>.Error("Failed to add the product.", Status.NetworkError);

            return OperationResult<PostProductDto>.Ok(product, Status.Created);
        }
    }
}
