using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagementAPI.Services.Settings;

namespace ProductManagementAPI.Services.Services
{
    public class AuthService(IOptions<JwtSettings> jwtSettings) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public string? GetAuthenticationToken(string username, string password)
        {
            // Hard-coded credentials
            if (username != "Market" || password != "NewPassword000")
                return null;

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSettings.SecretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
