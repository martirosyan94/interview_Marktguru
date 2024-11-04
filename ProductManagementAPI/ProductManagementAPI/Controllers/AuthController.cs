using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Services;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("Token")]
        [AllowAnonymous]
        public IActionResult GetAuthToken(AuthCredentialsDto credentials)
        {
            var token = authService.GetAuthenticationToken(credentials.Username, credentials.Password);
            if (token is null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
