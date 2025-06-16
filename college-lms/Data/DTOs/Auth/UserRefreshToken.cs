namespace college_lms.Data.DTOs.Auth;

public class UserRefreshToken
{
    public required int UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime Expires { get; set; }
}
