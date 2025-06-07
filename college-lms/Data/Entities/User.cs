using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace college_lms.Data.Entities;

public class User : IdentityUser
{

    [Required]
    public string Last_name { get; set; }

    [Required]
    public string First_name { get; set; }

    public string? Patronymic { get; set; }

    public bool Is_teacher { get; set; }

    public bool Is_admin { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Group> Groups { get; set; } = new();
    public List<AttendanceMark> AttendanceMarks { get; set; } = new();

    public List<Lesson> Lessons { get; set; } = new();
}