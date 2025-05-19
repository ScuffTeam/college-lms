using System.ComponentModel.DataAnnotations;

namespace college_lms;

public class AppSettings
{
    [Required(ErrorMessage = "SecretKey is required.")]
    public string SecretKey { get; set; } = default!;

    [Required(ErrorMessage = "Database connection string is required.")]
    public ConnectionStringsSection ConnectionStrings { get; set; } = default!;
}

public class ConnectionStringsSection
{
    [Required]
    public string DefaultDb { get; set; } = default!;
}
