using System.ComponentModel.DataAnnotations;

namespace college_lms;

public class AppOptions
{
    public const string Name = "AppOptions";

    [Required(ErrorMessage = "SecretKey is required.")]
    public string SecretKey { get; set; } = default!;

    [Required(ErrorMessage = "Database connection string is required.")]
    public DatabaseOptions DatabaseOptions { get; set; } = default!;
    public string RedisConnection { get; set; } = default!;
}

public class DatabaseOptions
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string Database { get; set; } = default!;

    [Required]
    public string Host { get; set; } = default!;

    [Required]
    public string Port { get; set; } = default!;
}
