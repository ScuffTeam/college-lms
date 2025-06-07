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
    public int Student_id { get; set; }
    public User User { get; set; }

    [Required]
    public int Lesson_id { get; set; }
    public Lesson Lesson { get; set; }

    public int? Grade { get; set; }

    public Attendance? Attendance { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime UpdatedAt { get; set; }
}