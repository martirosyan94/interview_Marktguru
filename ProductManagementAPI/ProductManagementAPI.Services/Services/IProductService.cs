using ProductManagementAPI.Data;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Models.Response;

namespace ProductManagementAPI.Services.Services
{
    public interface IProductService
    {
        Task<OperationResult<PostProductDto>> AddProductAsync(PostProductDto product, CancellationToken cancellationToke);
        Task<OperationResult<IEnumerable<GetProductDto>>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<OperationResult<GetProductDetailDto>> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task<OperationResult<UpdateProductDto>> UpdateProductAsync(int id, UpdateProductDto updateProduct, CancellationToken cancellationToken);
        Task<OperationResult> DeleteProductAsync(int id, CancellationToken cancellationToken);
    }
}
