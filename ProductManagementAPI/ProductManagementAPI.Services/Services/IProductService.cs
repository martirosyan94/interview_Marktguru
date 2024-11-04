using ProductManagementAPI.Data;
using ProductManagementAPI.Services.Models.Request;

namespace ProductManagementAPI.Services.Services
{
    public interface IProductService
    {
        Task<OperationResult<PostProductDto>> AddProductAsync(PostProductDto product, CancellationToken cancellationToke);
    }
}
