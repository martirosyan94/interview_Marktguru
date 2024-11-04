using ProductManagementAPI.Data.Models;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Models.Response;

namespace ProductManagementAPI.Services.Utilities
{
    public static class Mapping
    {
        public static Product ToProductEntityModel(this PostProductDto productDto)
        {
            return new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Available = productDto.Available,
                Description = productDto.Description,
                DateCreated = DateTime.UtcNow
            };
        }

        public static GetProductDto ToGetProductDto(this Product product)
        {
            return new GetProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Available = product.Available
            };
        }
    }
}
