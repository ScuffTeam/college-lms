namespace college_lms.Data.DTOs.Auth;

public class TokenResponse
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}
