namespace college_lms.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponse> RegisterAsync(string email, string password);
    Task<TokenResponse> LoginAsync(string email, string password);
}
