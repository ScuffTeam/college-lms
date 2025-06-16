using System.ComponentModel.DataAnnotations;

namespace college_lms.Data.Entities;

public enum Status
{
    Active,
    Passed,
    Verified,
    Expired,
}

public class Homework : EntityBase
{
    [Required]
    public int LessonId { get; set; }
    public required Lesson Lesson { get; set; }

    public string? Comment { get; set; }

    [Required]
    public required string Exercise { get; set; }

    public string? Solution { get; set; }

    [Required]
    public Status Status { get; set; }

    [Required]
    public DateOnly Deadline { get; set; }
}
