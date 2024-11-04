using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Services;

namespace ProductManagementAPI.Controllers
{
    namespace ProductManagementAPI.Controllers
    {
        [ApiController]
        [Produces("application/json")]
        [ApiVersion("1")]
        [Route("api/v1/[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _productService;

            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }

            [HttpPost]
            public async Task<IActionResult> AddProduct(PostProductDto product, CancellationToken cancellationToken)
            {
                var response = await _productService.AddProductAsync(product, cancellationToken);

                if (response.Success)
                    return StatusCode((int)response.Status, response.Data);

                return StatusCode((int)response.Status, response.ErrorMessage);
            }
        }
    }
}
