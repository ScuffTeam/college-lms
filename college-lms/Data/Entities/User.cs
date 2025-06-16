using System.ComponentModel.DataAnnotations;
using college_lms.Data.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace college_lms.Data.Entities;

public class User : IdentityUser<int>, IWithTimestamps, IIdentifiable
{
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public bool IsTeacher { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Group> Groups { get; set; } = [];
    public ICollection<AttendanceMark> AttendanceMarks { get; set; } = [];
    public ICollection<Lesson> Lessons { get; set; } = [];
}
