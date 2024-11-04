using ProductManagementAPI.Data;
using ProductManagementAPI.Data.Models;
using ProductManagementAPI.Data.RepositoryInterfaces;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Models.Response;
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

        public async Task<OperationResult<IEnumerable<GetProductDto>>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var allProducts = (await _productRepository.GetAllProductsAsync(cancellationToken)).ToList();

            if (!allProducts.Any())
                return OperationResult<IEnumerable<GetProductDto>>.Error("There are no available products", Status.NotFound);

            return OperationResult<IEnumerable<GetProductDto>>.Ok(allProducts.Select(p => p.ToGetProductDto()));
        }

        public async Task<OperationResult<GetProductDetailDto>> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var productResult = await LoadProductByIdAsync(id, cancellationToken);

            if (!productResult.Success)
                return OperationResult<GetProductDetailDto>.Error(productResult.ErrorMessage, productResult.Status);

            return OperationResult<GetProductDetailDto>.Ok(productResult.Data!.ToGetProductDetailDto());
        }

        public async Task<OperationResult<UpdateProductDto>> UpdateProductAsync(int id, UpdateProductDto updateProduct, CancellationToken cancellationToken)
        {
            var productResult = await LoadProductByIdAsync(id, cancellationToken);
            if (!productResult.Success)
                return OperationResult<UpdateProductDto>.Error(productResult.ErrorMessage, productResult.Status);

            var existingProduct = updateProduct.ToProductEntityModel(id, productResult.Data!.RowVersion);

            _productRepository.UpdateProduct(existingProduct);
            var success = await _productRepository.SaveChangesAsync(cancellationToken);

            if (!success)
                return OperationResult<UpdateProductDto>.Error("Failed to update the product.", Status.NetworkError);

            return OperationResult<UpdateProductDto>.Ok(updateProduct);
        }

        private async Task<OperationResult<Product>> LoadProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id, cancellationToken);

            if (existingProduct is null)
                return OperationResult<Product>.Error($"The product with {id} not found", Status.NotFound);

            return OperationResult<Product>.Ok(existingProduct);
        }

        public async Task<OperationResult> DeleteProductAsync(int id, CancellationToken cancellationToken)
        {
            var productResult = await LoadProductByIdAsync(id, cancellationToken);
            if (!productResult.Success)
                return OperationResult.Error(productResult.ErrorMessage, productResult.Status);

            _productRepository.DeleteProduct(productResult.Data!);
            var success = await _productRepository.SaveChangesAsync(cancellationToken);

            if (!success)
                return OperationResult<UpdateProductDto>.Error("Failed to Delete the product.", Status.NetworkError);

            return OperationResult.Ok();
        }
    }
}
