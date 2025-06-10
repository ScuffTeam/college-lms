using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace college_lms.Data.Entities;

public class User : IdentityUser
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

    public List<Group> Groups { get; set; } = new();
    public List<AttendanceMark> AttendanceMarks { get; set; } = new();

    public List<Lesson> Lessons { get; set; } = new();
}