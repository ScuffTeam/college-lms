namespace college_lms.Data.DTOs.Base;

public class ValidationErrorResponse : ErrorResponse
{
    public required IEnumerable<ValidationError> Errors { get; set; }
}

public class ValidationError
{
    public required string Field { get; set; }
    public string? Message { get; set; }
}
