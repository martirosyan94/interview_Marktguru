using ProductManagementAPI.Data.Models;
using ProductManagementAPI.Services.Models.Request;

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
    }
}
