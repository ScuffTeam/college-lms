using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace college_lms.Data.Entities;

public enum Attendance
{
    Present,
    Late,
    Absent,
}

public class AttendanceMark : EntityBase
{
    [Required]
    [ForeignKey(nameof(User))]
    public int StudentUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public required User User { get; set; }

    [Required]
    public int Lesson_id { get; set; }
    public required Lesson Lesson { get; set; }

    public int? Grade { get; set; }

    public Attendance? Attendance { get; set; }
}
