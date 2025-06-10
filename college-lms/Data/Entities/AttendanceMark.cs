using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;

public enum Attendance
{
    Present,
    Was_late,
    Absent
}


public class AttendanceMark
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }
    public required User User { get; set; }

    [Required]
    public int LessonId { get; set; }
    public required Lesson Lesson { get; set; }

    public int? Grade { get; set; }

    public Attendance? Attendance { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}