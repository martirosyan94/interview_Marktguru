namespace ProductManagementAPI.Services.Services
{
    public interface IAuthService
    {
        string? GetAuthenticationToken(string username, string password);
    }
}
