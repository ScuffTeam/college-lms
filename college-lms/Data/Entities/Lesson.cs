using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace college_lms.Data.Entities;

public class Lesson : EntityBase
{
    [Required]
    [ForeignKey(nameof(Teacher))]
    public int TeacherUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public required User Teacher { get; set; }

    [Required]
    [ForeignKey(nameof(Room))]
    public int RoomId { get; set; }
    public required Room Room { get; set; }

    [MaxLength(255)]
    public string? Topic { get; set; }

    [MaxLength(2047)]
    public string? Description { get; set; }

    [Required]
    public TimeOnly TimeStart { get; set; }

    [Required]
    public TimeOnly TimeEnd { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    [ForeignKey(nameof(ModulePart))]
    public int ModulePartId { get; set; }
    public required ModulePart ModulePart { get; set; }

    public ICollection<Homework> Homeworks { get; set; } = [];

    public ICollection<AttendanceMark> AttendanceMarks { get; set; } = [];

    public ICollection<Group> Groups { get; set; } = [];
}
