namespace ProductManagementAPI.Services.Models.Request
{
    public class AuthCredentialsDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
