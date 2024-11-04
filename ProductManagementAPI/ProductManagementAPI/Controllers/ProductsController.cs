using Microsoft.AspNetCore.Authorization;
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

            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
            {
                var response = await _productService.GetAllProductsAsync(cancellationToken);

                if (response.Success)
                    return Ok(response.Data);

                return StatusCode((int)response.Status, response.ErrorMessage);
            }

            [HttpGet("{id:int}")]
            [AllowAnonymous]
            public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
            {
                var response = await _productService.GetProductByIdAsync(id, cancellationToken);

                if (response.Success)
                    return Ok(response.Data);

                return StatusCode((int)response.Status, response.ErrorMessage);
            }

            [HttpPut("{id:int}")]
            public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto product, CancellationToken cancellationToken)
            {
                var response = await _productService.UpdateProductAsync(id, product, cancellationToken);

                if (response.Success)
                    return Ok(response.Data);

                return StatusCode((int)response.Status, response.ErrorMessage);
            }
        }
    }
}
