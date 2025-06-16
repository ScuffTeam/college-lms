using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace college_lms.Data.Entities;

public class Lesson : EntityBase
{
    [Required]
    [ForeignKey(nameof(Teacher))]
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public int TeacherUserId { get; set; }
    public required User Teacher { get; set; }

    [Required]
    public int GroupId { get; set; }
    public required Group Group { get; set; }

    [Required]
    public int Room_id { get; set; }
    public Room Room { get; set; }

    [MaxLength(255)]
    public string? Topic { get; set; }

    [MaxLength(2047)]
    public string? Description { get; set; }

    [Required]
    public TimeOnly Time_Start { get; set; }

    [Required]
    public TimeOnly Time_end { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public int Pair_number { get; set; }

    public ICollection<Homework> Homeworks { get; set; } = [];

    public ICollection<AttendanceMark> AttendanceMarks { get; set; } = [];

    public ICollection<Group> Groups { get; set; } = [];
}
